package com.evanblondeau.tuto_bl;
import android.Manifest;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Document;

import java.io.InputStream;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Set;
import java.util.UUID;
import java.util.regex.Pattern;

import de.nitri.gauge.Gauge;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "Main Activity";
    int REQUEST_ENABLE_BT = 1;
    BluetoothAdapter mBluetoothAdapter;

    private static MainActivity instance;
    Button buttonessai;
    BluetoothConnectionService mBluetoothConnectionService;

    private static final UUID MY_UUID= UUID.fromString("00001800-0000-1000-8000-00805f9b34fb");
    //fa87c0d0-afac-11de-8a39-0800200c9a66
    BluetoothDevice mBluetoothDevice;

    TextView text_test;
    StringBuilder messages;
    LinearLayout OutString;
    Button btnEnableDisable_discoverable;
    Button btnStartConnection;
    Button btnSend;
    EditText editText;
    TextView textOut;
    Context mContext ;
    ProgressBar progressBar;
    TextView text_id;
    TextView text_con_state;
    TextView strIn;
    LinearLayout InString;
    Gauge gaugeBat;
    Gauge gaugeVit;
    Gauge gaugeRpm;
    Gauge gaugeLiq;
    TextView text_temp;
    TextView text_lum;
    TextView text_hum;
    TextView text_dist_ar;
    TextView text_dist_av;
    TextView text_pression;

    Document doc;

    ScrollView scrollOut;
    ScrollView scrollIn;
    char id_view=0;

    private Menu menu;

    public ArrayList<BluetoothDevice> mBTDevice = new ArrayList<>();
    public ArrayList<BluetoothDevice> mBTPairedDevice = new ArrayList<>();
    public DeviceListAdapter mDeviceListAdapter;

    ListView listDevice;
    ListView listPairedDevice;

    ArrayList<ArrayList<ArrayList<Object>>> listSprite = new ArrayList<ArrayList<ArrayList<Object>>>();
    ArrayList<ArrayList<Object>> listgauge = new ArrayList<>();
    ArrayList<Object> listgaugeinde = new ArrayList<>();


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // btnEnableDisable_discoverable = findViewById(R.id.btn_enbale_disco);
        listDevice = findViewById(R.id.list);
        listPairedDevice = findViewById(R.id.listPaired);
        mBTDevice = new ArrayList<>();
        mBTPairedDevice = new ArrayList<>();

        instance=this;
        mContext=this;
        Toolbar toolbar = findViewById(R.id.toolbar);

        setSupportActionBar(toolbar);
        InString =findViewById(R.id.LinearIn);
        progressBar = findViewById(R.id.progressBar);
        progressBar.setProgress(0);
        progressBar.setProgressTintList(ColorStateList.valueOf(Color.RED));

        scrollOut = findViewById(R.id.LinearOutScr);
        scrollIn = findViewById(R.id.LinearInScr);

        text_id = findViewById(R.id.textid);
        text_con_state =findViewById(R.id.text_connect);

        LocalBroadcastManager.getInstance(this).registerReceiver(mReceiver,new IntentFilter("incomingMessage"));
        text_test = findViewById(R.id.texttest);
        messages = new StringBuilder();




        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        btnStartConnection = findViewById(R.id.Start);
        btnSend = findViewById(R.id.btnSend);
        editText =findViewById(R.id.Edit);

        btnStartConnection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startConnection();
            }
        });
        btnSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Context context = MainActivity.this;
                String str= editText.getText().toString();
                strIn = new TextView(context);
                String str2 = " > " +str;
                strIn.setText(str2);
                InString.addView(strIn);

                byte[]bytes = (str+"\r").getBytes(Charset.defaultCharset());
                mBluetoothConnectionService.write(bytes,str);
                editText.setText("");
                scrollIn.fullScroll(View.FOCUS_DOWN);
                scrollIn.fullScroll(View.FOCUS_DOWN);
            }
        });

        listDevice.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Log.d(TAG, "on click: you cliked on a device");
                mBluetoothAdapter.cancelDiscovery();

                Log.d(TAG, "on click: you cliked on a device");
                String deviceName = mBTDevice.get(position).getName();
                String deviceAdress = mBTDevice.get(position).getAddress();
                Log.d(TAG,"on click pair: "+deviceName + " : "+ deviceAdress);

                //create appairage
                if(Build.VERSION.SDK_INT> Build.VERSION_CODES.JELLY_BEAN_MR2){
                    Log.d(TAG, "Trying to pair with " + deviceName);
                    if(mBTDevice.get(position).getBondState()==12)
                    {
                        Toast.makeText(MainActivity.this, "Already bounded to " + deviceName , Toast.LENGTH_SHORT).show();
                        text_id.setText(deviceName);
                        enableBtnCon(true);
                    }else{
                        mBTDevice.get(position).createBond();
                    }
                    mBluetoothDevice = mBTDevice.get(position);
                    mBluetoothConnectionService = new BluetoothConnectionService((MainActivity.this));

                }
            }});

        listPairedDevice.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                String deviceName = mBTPairedDevice.get(position).getName();
                String deviceAdress = mBTPairedDevice.get(position).getAddress();
                Log.d(TAG,"on click already paired: "+deviceName + " : "+ deviceAdress);

                Toast.makeText(MainActivity.this, "Bounded to " + deviceName , Toast.LENGTH_SHORT).show();
                text_id.setText(deviceName);

                mBluetoothConnectionService = new BluetoothConnectionService((MainActivity.this));
                mBluetoothDevice =mBTPairedDevice.get(position);
                enableBtnCon(true);
            }
        });
        IntentFilter filterpair = new IntentFilter(BluetoothDevice.ACTION_BOND_STATE_CHANGED);
        registerReceiver(mBroadcastReceiver4, filterpair );

        pairedDevices(this);
        enableBtnCon(false);
        enableBtnSend(false);
    }

    public void enableBtnCon(boolean val){
        if(val){
            btnStartConnection.setEnabled(true);
            Log.d(TAG, "enableBtnSend: enable");
        }
        else{
            btnStartConnection.setEnabled(false);
            Log.d(TAG, "enableBtnCon: disable");
        }
    }

    public void enableBtnSend(boolean val){
        if(val){
            btnSend.setEnabled(true);
            Log.d(TAG, "enableBtnSend: enable");
        }
        else{
            btnSend.setEnabled(false);
            Log.d(TAG, "enableBtnSend: diasble");
        }
    }


    public static MainActivity getInstance() {
        return instance;
    }

    // Create a BroadcastReceiver for ACTION CHANGE BLE.
    private final BroadcastReceiver mBroadcastReceiver = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            if (action.equals(mBluetoothAdapter.ACTION_STATE_CHANGED)) {
                final int state= intent.getIntExtra(BluetoothAdapter.EXTRA_STATE,mBluetoothAdapter.ERROR);
                switch (state){
                    case BluetoothAdapter.STATE_OFF:
                        Log.d(TAG,"OnReceive: STATE OFF");
                        enableBtnCon(false);
                        enableBtnSend(false);
                        menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.baseline_bluetooth_disabled_white_18dp));
                        break;
                    case BluetoothAdapter.STATE_TURNING_OFF:
                        Log.d(TAG,"OnReceive: STATE_TURNING_OFF");
                        break;
                    case BluetoothAdapter.STATE_ON:
                        Log.d(TAG,"OnReceive: STATE_ON");
                        menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.baseline_bluetooth_white_18dp));
                        enableDiscoverable();
                        pairedDevices(context);
                        break;
                    case BluetoothAdapter.STATE_TURNING_ON:
                        Log.d(TAG,"OnReceive: STATE_TURNING_ON");
                        break;

                }
            }
        }
    };

    // Create a BroadcastReceiver for ACTION CHANGE BLE.
    private final BroadcastReceiver mBroadcastReceiver2 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            if (action.equals(mBluetoothAdapter.ACTION_SCAN_MODE_CHANGED)) {
                int mode= intent.getIntExtra(BluetoothAdapter.EXTRA_SCAN_MODE,mBluetoothAdapter.ERROR);
                switch (mode){
                    case BluetoothAdapter.SCAN_MODE_CONNECTABLE:
                        Log.d(TAG,"mBroadcastReceiver2: Discorability Disable. Able to receive connections");

                        break;
                    case BluetoothAdapter.SCAN_MODE_NONE:
                        Log.d(TAG,"mBroadcastReceiver2: Discoverability Disable. Nor able to receive connection");
                        break;
                    case BluetoothAdapter.STATE_CONNECTING:
                        Log.d(TAG,"mBroadcastReceiver2: Connecting");
                        break;
                    case BluetoothAdapter.STATE_CONNECTED:
                        Log.d(TAG,"mBroadcastReceiver2: Connected ");
                        break;
                }
            }
        }
    };

    @Override
    protected void onDestroy() {
        Log.d(TAG,"onDestroy Broadcast");
        super.onDestroy();
        unregisterReceiver(mBroadcastReceiver);
        unregisterReceiver(mBroadcastReceiver2);
        unregisterReceiver(mBroadcastReceiver3);
        unregisterReceiver(mBroadcastReceiver4);

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        if(id_view == 0 ){
            getMenuInflater().inflate(R.menu.menu_main, menu);
        }else if(id_view ==1)
        {
            getMenuInflater().inflate(R.menu.menu_gauge, menu);
        }


        this.menu=menu;
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_BLE) {
            Log.d(TAG,"OnClick: enabledisable BLE");
            enableDisableBT();
            return true;
        }
        else if(id==R.id.action_Disco){
            disco_Device();
        }
        else if(id==R.id.action_Gauge){
            listSprite = new ArrayList<ArrayList<ArrayList<Object>>>();


            id_view = 1;
            setContentView(R.layout.activity_sprite);
            Toolbar toolbar = findViewById(R.id.toolbar);

            setSupportActionBar(toolbar);
            Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
            getSupportActionBar().setDisplayShowHomeEnabled(true);

            gaugeLiq = findViewById(R.id.gaugeliquide);
            listgaugeinde = new ArrayList<>();
            listgaugeinde.add("5C");
            listgaugeinde.add(gaugeLiq);
            listgauge.add(listgaugeinde);

            gaugeBat = findViewById(R.id.gaugeBaterry);

            gaugeRpm = findViewById(R.id.gaugeRPM);
            listgaugeinde = new ArrayList<>();
            listgaugeinde.add("0C");
            listgaugeinde.add(gaugeRpm);
            listgauge.add(listgaugeinde);

            gaugeVit = findViewById(R.id.gaugeVit);
            listgaugeinde = new ArrayList<>();
            listgaugeinde.add("0D");
            listgaugeinde.add(gaugeVit);
            listgauge.add(listgaugeinde);

            listSprite.add(listgauge);

             text_temp = findViewById(R.id.Text_Temp);
             text_lum = findViewById(R.id.Text_luminosité);
             text_hum= findViewById(R.id.Text_hummidité);
             text_dist_ar=findViewById(R.id.Text_dist_ar);
             text_dist_av=findViewById(R.id.Text_dist_av);
             text_pression=findViewById(R.id.Text_pression_athmos);



        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public boolean onSupportNavigateUp() {
        id_view = 0;
        setContentView(R.layout.activity_main);

        Toolbar toolbar = findViewById(R.id.toolbar);

        setSupportActionBar(toolbar);
        return true;
    }

        BroadcastReceiver mReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                String text = intent.getStringExtra("theMessage");
                Log.d(TAG, "onReceive : " +text);
                if(id_view==0){

                    OutString = findViewById(R.id.LinearOut);
                    messages.append(" > ").append(text);
                    textOut= new TextView(context);
                    textOut.setText(messages);
                    OutString.addView(textOut);
                    scrollOut.fullScroll(View.FOCUS_DOWN);

                }else if(id_view==1){
                    //correspondancePID(text);
                }

                messages=new StringBuilder();
            }
        };



    //created method for starting connection
    //need to be paired first
    public void startConnection(){
        setBtnStartConnection(mBluetoothDevice,MY_UUID);
    }

    public void setBtnStartConnection(BluetoothDevice device, UUID uuid){
        Log.d(TAG, "setBtnStartConnection: Initialising RFCOM Ble connection");
        progressBar.setProgress(25);
        mBluetoothConnectionService.startClient(device,uuid);
    }

    // Create a BroadcastReceiver for ACTION PAIRED.
    private final BroadcastReceiver mBroadcastReceiver4 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            final String action = intent.getAction();

            if(action.equals(BluetoothDevice.ACTION_BOND_STATE_CHANGED)){
                BluetoothDevice mDevice = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                // paired
                if(mDevice.getBondState() == BluetoothDevice.BOND_BONDED){
                    Log.d(TAG,"mBroadcastReceiver4: BOND_BOUNDED");
                    mBluetoothDevice =mDevice;
                    Toast.makeText(context,"Device bounded", Toast.LENGTH_SHORT).show();
                    text_id.setText(mDevice.getName());
                    enableBtnCon(true);
                }
                //creating a bound(pair)
                if(mDevice.getBondState() == BluetoothDevice.BOND_BONDING){
                    Log.d(TAG,"mBroadcastReceiver4 : BOND_BOUNDING");
                    Toast.makeText(context,"Pairing to "+mDevice.getName(), Toast.LENGTH_SHORT).show();
                }
                //break a bound
                if(mDevice.getBondState() == BluetoothDevice.BOND_NONE){
                    Log.d(TAG,"mBroadcastReceiver4 : BOND_NONE");
                }
            }
        }
    };

    private void enableDisableBT() {
        if(mBluetoothAdapter== null){
            Log.d(TAG,"you dont have bluetooth capabilities");
        }
        if(!mBluetoothAdapter.isEnabled()){
            Intent enableBTIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
            startActivityForResult(enableBTIntent, REQUEST_ENABLE_BT);

            IntentFilter BTIntent = new IntentFilter(BluetoothAdapter.ACTION_STATE_CHANGED);
            registerReceiver(mBroadcastReceiver,BTIntent);
        }
        if(mBluetoothAdapter.isEnabled()){
            mBluetoothAdapter.disable();

            IntentFilter BTIntent = new IntentFilter(BluetoothAdapter.ACTION_STATE_CHANGED);
            registerReceiver(mBroadcastReceiver,BTIntent);
        }
    }

    public void enableDiscoverable() {
        Log.d(TAG,"btn disco : making device disco for 300 seconds");
        Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
        discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 300);
        startActivity(discoverableIntent);

        IntentFilter intentFilter = new IntentFilter(mBluetoothAdapter.ACTION_SCAN_MODE_CHANGED);
        registerReceiver(mBroadcastReceiver2,intentFilter);
    }

    public void disco_Device() {
        Log.d(TAG, "btn discover : Looking for unpaired devices");
        if(mBluetoothAdapter.isDiscovering()){
            mBluetoothAdapter.cancelDiscovery();
            Log.d(TAG, "btn discover : cancel discovery");
            //check permission
            checkBTpermission();
            mBluetoothAdapter.startDiscovery();
            IntentFilter discoverDeviceIntent = new IntentFilter(BluetoothDevice.ACTION_FOUND);
            registerReceiver(mBroadcastReceiver3,discoverDeviceIntent);
        }
        if(!mBluetoothAdapter.isDiscovering()){
            //check permission
            checkBTpermission();
            mBluetoothAdapter.startDiscovery();
            IntentFilter discoverDeviceIntent = new IntentFilter(BluetoothDevice.ACTION_FOUND);
            registerReceiver(mBroadcastReceiver3,discoverDeviceIntent);
        }
        Toast.makeText(this,"Discover on", Toast.LENGTH_SHORT).show();
    }

    // Create a BroadcastReceiver for ACTION CHANGE BLE.
    private final BroadcastReceiver mBroadcastReceiver3 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            final String action = intent.getAction();
            Log.d(TAG,"onReceive: action found mb3");

            if(action.equals(BluetoothDevice.ACTION_FOUND)){
                BluetoothDevice device = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                mBTDevice.add(device);
                Log.d(TAG,"on receive"+ device.getName() + ": " + device.getAddress());
                mDeviceListAdapter = new DeviceListAdapter(context,R.layout.device_adapter_view,mBTDevice);
                listDevice.setAdapter(mDeviceListAdapter);
            }
        }
    };

    /**
     * This method is required for all devices running API23+
     * Android must programmatically check the permissions for bluetooth. Putting the proper permissions
     * in the manifest is not enough.
    */
    private void checkBTpermission(){
        if(Build.VERSION.SDK_INT > Build.VERSION_CODES.LOLLIPOP){
            int permissionCheck = this.checkSelfPermission("Manifest.permission.ACCESS_FINE_LOCATION");
            permissionCheck += this.checkSelfPermission("Manifest.permission.ACCESS_COARSE_LOCATION");
            if (permissionCheck != 0) {

                this.requestPermissions(new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 1001); //Any number
            }
        }else{
            Log.d(TAG, "checkBTPermissions: No need to check permissions. SDK version < LOLLIPOP.");
        }
    }

    public void changeProgressBar(int value, String color){
        progressBar.setProgress(value);
        if(color.equals("GREEN")){
            progressBar.setProgressTintList(ColorStateList.valueOf(Color.GREEN));
        }else{
            progressBar.setProgressTintList(ColorStateList.valueOf(Color.RED));
        }
    }

    public void pairedDevices(Context context) {
        Set<BluetoothDevice> pairedDevices = mBluetoothAdapter.getBondedDevices();
        mBTPairedDevice = new ArrayList<>();
        if (pairedDevices.size() > 0) {
            // There are paired devices. Get the name and address of each paired device.
            for (BluetoothDevice device : pairedDevices) {
                String deviceName = device.getName();
                String deviceHardwareAddress = device.getAddress(); // MAC address
                mBTPairedDevice.add(device);
            }
            mDeviceListAdapter = new DeviceListAdapter(context,R.layout.device_adapter_view,mBTPairedDevice);
            listPairedDevice.setAdapter(mDeviceListAdapter);

        }
    }

    public void changeConnectionState(String value){
        if(value.equals("Connected")){
            text_con_state.setText("Connected");
        }else {
            text_con_state.setText("Disconnected");
        }
    }

    private static Pattern WHITESPACE_PATTERN = Pattern.compile("\\s");

    protected String removeAll(Pattern pattern, String input) {
        return pattern.matcher(input).replaceAll("");
    }

    private void correspondancePIDJSON(String text) {
        String dd = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        //Log.d(TAG, "correspondancePID: trim "+dd);
        String text2=  dd.substring(2);
        while(text2.length()>0) {
            //while str.length>0 Il faut savoir exactement le nombre d'octet que nous
            int i = 0;
            String iden = text2.substring(0, 2);
           // Log.d(TAG, "correspondancePID: id "+iden);
            String data = text2.substring(2);
           // Log.d(TAG, "correspondancePID: dat "+data);
            Long res;

            // attendons en rececption car dans les multiple PID request on receptionne tout en meme temp!!!!
            try {
                InputStream is = getAssets().open("PID.json");
                int size = is.available();
                byte[] buffer = new byte[size];
                is.read(buffer);
                is.close();

            while(i<size) {

                //"41 0C 4F FF 0D 08 33 5F"
                String myJson = new String(buffer, "UTF-8");

                JSONObject jsonComplet = new JSONObject(myJson);
                JSONArray array = new JSONArray(jsonComplet.getString("Service 01"));
                JSONObject obj = new JSONObject(array.getString(i));

                if(iden.equals(obj.getString("PID"))){
                    text2=data.substring(Integer.parseInt(obj.getString("nbBytes")));
                    Long valeur_sans_op = Long.parseLong(data.substring(0,Integer.parseInt(obj.getString("nbBytes"))), 16);
                   // Log.d(TAG, "correspondancePID: rpm traite " +valeur_sans_op);

                    switch (obj.getString("operations")){
                        case "false":
                            Long finalResult=valeur_sans_op;
                            Log.d(TAG, obj.getString("description") + " : " +finalResult + " " + obj.getString("unite"));
                            gaugeText(obj, finalResult,listSprite);
                            break;
                            
                        case "1":
                            finalResult =operation(1,valeur_sans_op,obj);
                            Log.d(TAG, obj.getString("description") + " : "  +finalResult+ " " + obj.getString("unite"));
                            gaugeText(obj, finalResult,listSprite);
                            break;
                            
                        case "2":
                            finalResult = operation(2,valeur_sans_op,obj);
                            Log.d(TAG, obj.getString("description") + " : "  +finalResult+ " " + obj.getString("unite"));
                            gaugeText(obj, finalResult,listSprite);
                            break;
                    }
                    break;
                }
                else {
                   // Log.d(TAG, "correspondancePIDJSON: false"+obj.getString("PID"));
                }
                i++;
            }
                is.close();

            } catch (Exception e) {
                e.printStackTrace();
                System.out.println(e);
            }
        }
    }

    private Long operation(int i, Long valeur_sans_op, JSONObject ob) throws JSONException {
        int k=1;
        Long value = valeur_sans_op;
        while(k<=i){
            switch(ob.getString("operateur"+k)){
                case "/":
                    value = value / Long.parseLong(ob.getString("value"+k));
                    break;
                case "-":
                    value = value - Long.parseLong(ob.getString("value"+k));
                    break;
                case "+":
                    value = value + Long.parseLong(ob.getString("value"+k));
                    break;
                case "*":
                    value = value * Long.parseLong(ob.getString("value"+k));
                    break;
            }
            k++;
        }
        return value;
    }

    private void gaugeText(JSONObject object, Long value, List<ArrayList<ArrayList<Object>>> listPrint) throws JSONException {
        Log.d(TAG, "gaugeText 1 : "+listPrint);
        for(int o = 0; o<=listPrint.size();o=o+2){
            if(object.getString("print").equals("gauge")){

                Log.d(TAG, "gaugeText 2 : "+listPrint.get(0));
               for (int i = 0; i<= listPrint.get(0).size(); i++){
                   Log.d(TAG, "gaugeText 3 : "+listPrint.get(0).get(i));
                if(listPrint.get(0).get(i).get(0).equals(object.getString("PID"))){
                    Log.d(TAG, "gaugeText: hello herre");
                    Gauge d = (Gauge) listPrint.get(0).get(i).get(1);
                    d.moveToValue(value);
                    break;
                }
                }
            }
        }


    }

   /* private void correspondancePID(String text) {
        String dd = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        Log.d(TAG, "correspondancePID: trim "+dd);
        String text2=  dd.substring(2);
        //Log.d(TAG, "correspondancePID: tex2 "+text2);
        while(text2.length()>0) {
            //while str.length>0 Il faut savoir exactement le nombre d'octet que nous
            // attendons en rececption car dans les multiple PID request on receptionne tout en meme temp!!!!

            String iden = text2.substring(0, 2);
            Log.d(TAG, "correspondancePID: id "+iden);
            String data = text2.substring(2);
            Log.d(TAG, "correspondancePID: dat "+data);
            Long res;
            switch (iden) {
                //RPM 2Hex
                case "0C":
                    text2=data.substring(4);
                    Log.d(TAG, "correspondancePID: rpm traite " + Long.parseLong(data.substring(0,4), 16));
                    gaugeRpm.moveToValue((Long.parseLong(data.substring(0,4), 16) / 4));
                    break;

                //vitesse 1Hex
                case "0D":
                    text2=data.substring(2);
                    Log.d(TAG, "correspondancePID: vit traité " + Long.parseLong(data.substring(0,2), 16));
                    gaugeVit.moveToValue(Long.parseLong(data.substring(0,2), 16));
                    break;

                //pression athmos 1Hex
                case "33":
                    text2=data.substring(2);
                    Log.d(TAG, "correspondancePID: pression traité " + Long.parseLong(data.substring(0,2), 16));
                    text_pression.setText(String.valueOf(Long.parseLong(data.substring(0,2), 16)));
                    break;

                //temperature ambiante 1Hex
                case "46":
                    text2=data.substring(2);
                    Log.d(TAG, "correspondancePID: Temp " + Long.parseLong(data.substring(0,2), 16));
                    res = ((Long.parseLong(data.substring(0,2), 16)) - 40);
                    text_temp.setText(String.valueOf(res));
                    break;

                // Engine oil temperature 1Hex
                case "5C":
                    text2=data.substring(2);
                    Log.d(TAG, "correspondancePID: Temp oil " + Long.parseLong(data.substring(0,2), 16));
                    Long res_oil = ((Long.parseLong(data.substring(0,2), 16)) - 40);
                    gaugeLiq.moveToValue(res_oil);
                    break;
            }
        }
    }
*/
    public void testbutton(View view) {
        Log.d(TAG, "onClick: dd");
        loop();
    }


    public void test(View view){
      //  JSONfile();
    }

    public void loop2()
    {
        new Thread(new Runnable() {
            public void run() {
                // loop until the thread is interrupted
                while (!Thread.currentThread().isInterrupted()) {
                    byte[]bytes = ("01 0C"+"\r").getBytes(Charset.defaultCharset());
                    mBluetoothConnectionService.write(bytes,"01 0C");
                    try {
                        Thread.sleep(250);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }
        }).start();
    }


    public void loop()
    {
        new Thread(new Runnable() {
            public void run() {
                // loop until the thread is interrupted
                while (!Thread.currentThread().isInterrupted()) {

                    correspondancePIDJSON("41 0C 4F FF 0D 08 33 5F 46 02 5C 40");

                    try {
                        Thread.sleep(5000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    correspondancePIDJSON("41 0C 00 FF 0D 80 33 51 46 80 5C 8F");

                    try {
                        Thread.sleep(5000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }

                }
            }
        }).start();
    }
}
