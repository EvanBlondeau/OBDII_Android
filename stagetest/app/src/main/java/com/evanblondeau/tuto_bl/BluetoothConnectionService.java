package com.evanblondeau.tuto_bl;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.content.Context;
import android.content.Intent;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;
import android.widget.ProgressBar;
import android.widget.Toast;
import com.github.pires.obd.commands.protocol.ObdResetCommand;
import com.github.pires.obd.exceptions.MisunderstoodCommandException;
import com.github.pires.obd.exceptions.NoDataException;
import com.github.pires.obd.exceptions.UnableToConnectException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.charset.Charset;
import java.util.UUID;
import java.util.regex.Pattern;
import needle.Needle;
import static java.lang.System.in;

public class BluetoothConnectionService {

    private static final String TAG = "BluetoothConnectionService";

    private static final String NAME = "BluetoothChatInsecure";

    private static final UUID MY_UUID= UUID.fromString("00001800-0000-1000-8000-00805f9b34fb");

    private final BluetoothAdapter mBluetoothAdapter;
    Context mContext;

    private AcceptThread mInsecureAcceptThread;

    private ConnectThread mConnectThread;
    private BluetoothDevice mDevice;
    private UUID deviceUUID;
    private ProgressBar mProgressBar;

    private static Pattern WHITESPACE_PATTERN = Pattern.compile("\\s");

    private static Pattern BUSINIT_PATTERN = Pattern.compile("(BUS INIT)|(BUSINIT)|(\\.)");

    private ConnectedThread mConnectedThread;

    private String message_denvoie;

    public BluetoothConnectionService(Context context) {
        mContext=context;

        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        start();
    }

    private class AcceptThread extends Thread{
        //local server socket
        private  final BluetoothServerSocket mServerSocket;

        public AcceptThread(){
            BluetoothServerSocket tmp = null;

            try {
                tmp = mBluetoothAdapter.listenUsingInsecureRfcommWithServiceRecord(NAME,MY_UUID);
                Log.d(TAG, "AcceptThread : Setting up server using "+ MY_UUID);
            } catch (IOException e) {
                Log.d(TAG,"Accept Thread IOexception " + e.getMessage());
            }

            mServerSocket=tmp;
        }

        public void run(){
            Log.d(TAG, "run AcceptThread Running ");
            BluetoothSocket socket = null;
            Log.d(TAG,"run RFCOM server socket start");

            //This is a blocking call and will return on a
            //successful connection or exception
            try {
                Log.d(TAG,"run RFCOM server socket start");
                socket = mServerSocket.accept();

                Log.d(TAG,"run RFCOM server socket accepted connection ");
            } catch (IOException e) {
                Log.d(TAG,"Server Thread IOexception " + e.getMessage());
            }

            if(socket!=null){
                connected(socket,mDevice);
            }

            Log.d(TAG,"End Accept Thread");
        }
        public void cancel() {
            Log.d(TAG,"cancel: Canceling AcceptThread");
            try {
                mServerSocket.close();
            } catch (IOException e) {
                Log.e(TAG, "close() of Accepthread Server socket failed " +e.getMessage());
            }
        }
    }

    /**
     * This thread runs while attempting to make an outgoing connection
     * with a device. It runs straight through; the connection either
     * succeeds or fails.
     */
    private class ConnectThread extends Thread {
        private BluetoothSocket mSock;

        public ConnectThread(BluetoothDevice device, UUID uuid){
            Log.d(TAG,"ConnectThread: Started");
            mDevice = device;
            deviceUUID = uuid;
        }

        public void run(){
            BluetoothSocket tmp = null;
            Log.d(TAG,"run: mConnectThread");

            try {
                Log.d(TAG,"ConnectThread: Trying to create InsecureRFsocket using uuid:"+ MY_UUID);
                tmp = mDevice.createInsecureRfcommSocketToServiceRecord(deviceUUID);

            } catch (IOException e) {
                Log.d(TAG,"ConnectThread: Could not create Socket IOexception:" +e.getMessage());
            }
            mSock=tmp;

            //Always cancel discovery
            mBluetoothAdapter.cancelDiscovery();

            // Make a connection to the BluetoothSocket
            try {
                // This is a blocking call and will only return on a
                // successful connection or an exception
                Needle.onMainThread().execute(new Runnable() {@Override public void run() {
                    MainActivity.getInstance().changeProgressBar(50,"RED");
                    Toast.makeText(mContext, "Trying to connect to " + mDevice.getName(), Toast.LENGTH_SHORT).show(); }});
                mSock.connect();
                Needle.onMainThread().execute(new Runnable() {@Override public void run() {
                    MainActivity.getInstance().changeProgressBar(100,"GREEN");
                    MainActivity.getInstance().changeConnectionState("Connected");
                    Toast.makeText(mContext, "Connected to "+mDevice.getName(), Toast.LENGTH_SHORT).show();
                    MainActivity.getInstance().enableBtnSend(true);}});
                Log.d(TAG,"mConnectThread connected");
            } catch (IOException e) {
                try {
                    Log.e("","trying fallback...");

                    Needle.onMainThread().execute(new Runnable() {@Override public void run() { MainActivity.getInstance().changeProgressBar(75,"RED"); }});
                    //pas touché à cela, c'est très compliqué à comprendre , regarder sur internet si il y a des incompréhension
                    mSock =(BluetoothSocket) mDevice.getClass().getMethod("createRfcommSocket", new Class[] {int.class}).invoke(mDevice,1);
                    mSock.connect();

                    Needle.onMainThread().execute(new Runnable() {@Override public void run() {
                        MainActivity.getInstance().changeProgressBar(100,"GREEN");
                        MainActivity.getInstance().changeConnectionState("Connected");
                        Toast.makeText(mContext, "Connected to "+mDevice.getName(), Toast.LENGTH_SHORT).show();
                    MainActivity.getInstance().enableBtnSend(true);}});
                    Log.e("","Connected");
                }
                catch (Exception e2) {

                    Log.d(TAG,"ConnecteThread:  Could not connect to UUID: "+ MY_UUID);
                    Needle.onMainThread().execute(new Runnable() {@Override public void run() { MainActivity.getInstance().changeProgressBar(0,"RED"); }});
                    Needle.onMainThread().execute(new Runnable() {@Override public void run() { MainActivity.getInstance().changeConnectionState("Disconnected");
                        MainActivity.getInstance().enableBtnSend(false);}});
                    try {
                        mSock.close();
                        Log.d(TAG,"run : Closed the socket" + e.getMessage());
                    } catch (IOException e1) {
                        Log.d(TAG,"mConnectThread: Unable to close the connection in socket : "+e1.getMessage());
                    }
                }

            }
            connected(mSock,mDevice);
        }

        public void cancel() {
            Log.d(TAG,"cancel: Closing Client socket");
            try {
                mSock.close();
                
            } catch (IOException e) {
                Log.e(TAG, "close() of ConnectedSocket failed " +e.getMessage());
            }
        }
    }

    /**
     * Start the service
     */
    public synchronized void start(){
        Log.d(TAG,"start");

        if(mConnectThread != null){
            mConnectThread.cancel();
            mConnectThread=null;
        }
        if(mInsecureAcceptThread==null){
            mInsecureAcceptThread = new AcceptThread();
            mInsecureAcceptThread.start();
        }
    }

    public void startClient(BluetoothDevice device, UUID uuid){
        Log.d(TAG,"StartClient: start");
        mConnectThread = new ConnectThread(device,uuid);
        mConnectThread.start();
    }


    private class ConnectedThread extends Thread{

        private final BluetoothSocket mSockett;
        private final InputStream mInputStream;
        private final OutputStream mOutputStream;
        String msg="";
        public ConnectedThread(BluetoothSocket socket){
            Log.d(TAG,"ConnectedThread: starting");

            mSockett=socket;
            InputStream tmpIn=null;
            OutputStream tmpOut=null;

            //dismiss the progress bar because its etablished

            try {
                tmpIn=mSockett.getInputStream();
                tmpOut=mSockett.getOutputStream();
            } catch (IOException e) {
                e.printStackTrace();
            }
            mInputStream=tmpIn;
            mOutputStream=tmpOut;


        }

        public void run(){
          //  byte[] buffer =new byte[8192];//buffer store for the stream
           // int bytes; //bytes returned from read()
            //Keep listening to the INputStream until a exception error
            while(true){
                try {
                    byte[] buffer = new byte[1];
                    int bytes=mInputStream.read(buffer,0,buffer.length);
                    Log.d(TAG, "inputStreamBytes: " + bytes);
                    String s = new String(buffer);

                    StringBuilder res = new StringBuilder();

                    byte b=0;
                    // read until '>' arrives OR end of stream reached
                    char c;
                    // -1 if the end of the stream is reached
                    //c = (char) bytes;


                    for(int i = 0; i < s.length(); i++) {
                        char x = s.charAt(i);
                        //0x3e => ">"
                        if (x == 0x3e) {
                            Log.d(TAG, "run: input " +msg);
                            String OutFinal = msg.substring(message_denvoie.length()) ;

                            Intent incomingMessageIntent = new Intent("incomingMessage");
                            incomingMessageIntent.putExtra("theMessage", OutFinal);
                            LocalBroadcastManager.getInstance(mContext).sendBroadcast(incomingMessageIntent);

                            msg = "";
                        }else{
                            msg = msg + x;
                        }
                    }

                  /*  Log.d(TAG, "inputStream incoming: " + incomingMessage);
                    while (((b = (byte) mInputStream.read()) > -1)) {
                        c = (char) b;
                        if (c == '>') // read until '>' arrives
                        {

                            Log.d(TAG, "inputStream: " + res.toString());
                            String Out_final;
                            //Out_final = removeall(message_denvoie,res.toString());
                            //Log.d(TAG, "run: input " +Out_final);
                            String dd = removeAll(BUSINIT_PATTERN, res.toString()); //removes all [ \t\n\x0B\f\r]
                             Log.d(TAG, "run: input " +dd);
                            Intent incomingMessageIntent = new Intent("incomingMessage");
                            incomingMessageIntent.putExtra("theMessage", dd);
                            LocalBroadcastManager.getInstance(mContext).sendBroadcast(incomingMessageIntent);


                            break;
                        }
                        res.append(c);
                    }*/




                } catch (IOException e) {

                    Log.d(TAG, "Error inputStream" + e.getMessage());
                    cancel();
                    break;
                }
            }
        }


       protected String replaceAll(Pattern pattern, String input, String replacement) {
            return pattern.matcher(input).replaceAll(replacement);
        }

        protected String removeAll(Pattern pattern, String input) {
            return pattern.matcher(input).replaceAll("");
        }

        protected String removeall(String rem, String input){
            Log.d(TAG, "removeall: rem "+rem.length());
            Log.d(TAG, "removeall: in " +input.length());
            for(int i = 0; i<rem.length();i++){
                try{
                    char str = rem.charAt(i);
                    for(int k=0;k<rem.length();k++){
                        if(str== input.charAt(k)){
                            input = input.replace(String.valueOf(str),"");
                            break;
                        }
                    }
                }catch (StringIndexOutOfBoundsException e){e.getMessage();}

            }
            return input;
        }

        public void write(byte[] bytes){
            String text = new String(bytes, Charset.defaultCharset());
            Log.d(TAG, "write: Writing to OutpuStream : "+ text);
            try {
                mOutputStream.write(bytes);
                mOutputStream.flush();
                Log.d(TAG, "write: Write Bytes");
            } catch (IOException e) {
                Log.d(TAG, "write: IOexception "+ e.getMessage());
            }
        }

        public void cancel() {
            Log.d(TAG,"cancel: Closing Connected InOutput socket");
            try {
                Needle.onMainThread().execute(new Runnable() {@Override public void run() { MainActivity.getInstance().changeConnectionState("Disconnected");
                    MainActivity.getInstance().enableBtnSend(true);
                    MainActivity.getInstance().changeProgressBar(0,"RED");
                }});

                mSockett.close();
            } catch (IOException e) {
                Log.e(TAG, "close() of ConnectedSocket inputOutput failed " +e.getMessage());
            }
        }
    }

    private void connected(BluetoothSocket mSocket, BluetoothDevice mDevice) {
        Log.d(TAG, "connected: Starting");
        mConnectedThread = new ConnectedThread(mSocket);
        mConnectedThread.start();
    }


    // le second attribut est utilisé pour la lecture car le elm327 renvoie le message envoyé
    // donc on doit le supprimé à la reception et donc l'avoir en mémoire à l'envoie
    public void write(byte[] out,String str){
        Log.d(TAG, "write: Write Call ");
        message_denvoie = str;
        mConnectedThread.write(out);

    }


}
