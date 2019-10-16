package com.evanblondeau.tuto_bl;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import com.koushikdutta.async.AsyncServer;
import com.koushikdutta.async.callback.CompletedCallback;
import com.koushikdutta.async.http.WebSocket;
import com.koushikdutta.async.http.server.AsyncHttpServer;
import com.koushikdutta.async.http.server.AsyncHttpServerRequest;
import com.koushikdutta.async.http.server.AsyncHttpServerResponse;
import com.koushikdutta.async.http.server.HttpServerRequestCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.InputStream;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Enumeration;
import java.util.List;
import java.util.Objects;
import java.util.regex.Pattern;

import de.nitri.gauge.Gauge;
import needle.Needle;

public class GaugeClass extends AppCompatActivity {


//region Declaration variables
private static final String TAG = "Gauge Activity";
    char id_view = 0;

    private Menu menu;

    LinearLayout linearLayoutText;

    int number_add;

    BluetoothConnectionService mBluetoothConnectionService;
    DecryptMessage mDecryptMessage;

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

    ArrayList<ArrayList<ArrayList<Object>>> listSprite = new ArrayList<>();
    ArrayList<ArrayList<Object>> listGauge = new ArrayList<>();
    ArrayList<ArrayList<Object>> listText = new ArrayList<>();
    ArrayList<Object> listGaugeInde = new ArrayList<>();
    ArrayList<Object> listTextInde = new ArrayList<>();

    ArrayList<LinearLayout> Tab_add_Text = new ArrayList<>();

    AsyncHttpServer server;
    TextView tvIP, tvPort;
    LinearLayout Ll;
    TextView tt;
    List<WebSocket> _sockets;
    public static String SERVER_IP = "";
    public static final int SERVER_PORT = 5000;
    String message_receive;
    int indexlist=0;
    TextView text_sending_etat;
    TextView text_request;
    Thread d;
    String envoie_chaine="01 0C 05 04 0D 33";
    boolean runningmulti=false;
    ScrollView ScrolLog;

    PIDServerClass serverClass;

//endregion

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sprite);
        Toolbar toolbar = findViewById(R.id.toolbar);

      //  mDecryptMessage = transpo.getDecrypt();

      //  linearLayoutText = findViewById(R.id.layout_text);

        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        id_view = 1; // a integrer dans TRANSPO.JAVA pour le reutilisé si on a envie

        listSprite = new ArrayList<>();

        linearLayoutText = findViewById(R.id.layout_text);
        LocalBroadcastManager.getInstance(this).registerReceiver(mReceiver, new IntentFilter("incomingMessage"));
        setSupportActionBar(toolbar);
        Objects.requireNonNull(getSupportActionBar()).setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        listGauge = new ArrayList<>();
        gaugeLiq = findViewById(R.id.gaugeliquide);
        addTextGauge(gaugeLiq, "5C", listGaugeInde, listGauge);

        gaugeBat = findViewById(R.id.gaugeBaterry);//pas encore d'info ici

        gaugeRpm = findViewById(R.id.gaugeRPM);
        addTextGauge(gaugeRpm, "0C", listGaugeInde, listGauge);

        gaugeVit = findViewById(R.id.gaugeVit);
        addTextGauge(gaugeVit, "0D", listGaugeInde, listGauge);

        listSprite.add(listGauge);

        listText = new ArrayList<>();

        text_temp = findViewById(R.id.Text_Temp);//ok
        addTextGauge(text_temp, "46", listTextInde, listText);

        text_pression = findViewById(R.id.Text_pression_athmos);//ok
        addTextGauge(text_pression, "33", listTextInde, listText);

        text_lum = findViewById(R.id.Text_luminosité);//pas encore d'info ici
        text_hum = findViewById(R.id.Text_hummidité);//pas encore d'info ici
        text_dist_ar = findViewById(R.id.Text_dist_ar);//pas encore d'info ici
        text_dist_av = findViewById(R.id.Text_dist_av);//pas encore d'info ici
        text_sending_etat= findViewById(R.id.Text_sending_state);
        text_request= findViewById(R.id.text_request);
        text_sending_etat.setText("Stop");
        text_request.setText(envoie_chaine);

        listSprite.add(listText);
        ScrolLog = findViewById(R.id.ScrollLog);
        //mDecryptMessage.setListSprite(listSprite);

        number_add=0;

       mBluetoothConnectionService= blestore.getInstance().getMyBlueComms();

        instance=this;

//region Serveur web


        serverClass = new PIDServerClass(Long.valueOf(0),Long.valueOf(0),Long.valueOf(0));


        tvIP = findViewById(R.id.tvIP);
        tvPort = findViewById(R.id.tvPort);
        Ll = findViewById(R.id.linear5);

        tt = new TextView(getInstance());
        tt.setText(" Serveur deployed " );
        Ll.addView(tt);

        server = new AsyncHttpServer();
        _sockets = new ArrayList<WebSocket>();

        server.get("/", new HttpServerRequestCallback() {
            @Override
            public void onRequest(AsyncHttpServerRequest request, AsyncHttpServerResponse response) {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        tt = new TextView(getInstance());
                        tt.setText(" ClientHttp >  requete /" );
                        Ll.addView(tt);
                        ScrolLog.fullScroll(View.FOCUS_DOWN);
                    }
                });
                final JSONObject json = new JSONObject();

                try {
                    json.put("RPM", serverClass.GetRpm());
                    json.put("Speed", serverClass.GetSpeed());
                    json.put("Pressure", serverClass.GetPressure());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                Log.d(TAG, "onRequest: send " +json);
                response.send(json);
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        tt = new TextView(getInstance());
                        tt.setText(" ServerHttp > "+ json);
                        Ll.addView(tt);
                        ScrolLog.fullScroll(View.FOCUS_DOWN);
                    }
                });
            }
        });

        server.setErrorCallback(new CompletedCallback() {
            @Override
            public void onCompleted(Exception ex) {

            }
        });

        server.websocket("/live", new AsyncHttpServer.WebSocketRequestCallback() {
            @Override
            public void onConnected(final WebSocket webSocket, AsyncHttpServerRequest request) {
                _sockets.add(webSocket);
                for (WebSocket d : _sockets) {
                    Log.d(TAG, "Socket : "+d.toString());
                }
                Log.d(TAG, "onConnected: add web socket  ");

                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        tt = new TextView(getInstance());
                        tt.setText(" new Client connected " );
                        Ll.addView(tt);
                        ScrolLog.fullScroll(View.FOCUS_DOWN);
                    }
                });

                //Use this to clean up any references to your websocket
                webSocket.setClosedCallback(new CompletedCallback() {
                    @Override
                    public void onCompleted(Exception ex) {
                        try {
                            if (ex != null)
                                Log.e("WebSocket", "An error occurred", ex);
                        } finally {
                            _sockets.remove(webSocket);
                            runOnUiThread(new Runnable() {

                                @Override
                                public void run() {
                                    tt = new TextView(getInstance());
                                    tt.setText(" Client close " );
                                    Ll.addView(tt);
                                    ScrolLog.fullScroll(View.FOCUS_DOWN);
                                }
                            });

                        }
                    }
                });

                webSocket.setStringCallback(new WebSocket.StringCallback() {
                    @Override
                    public void onStringAvailable(String s) {
                        Log.d(TAG, "onStringAvailable: hhhhhhhhhhhhhh" + s);
                        message_receive =s;
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                tt = new TextView(getInstance());
                                tt.setText(" Client > " +message_receive);
                                Ll.addView(tt);
                                ScrolLog.fullScroll(View.FOCUS_DOWN);
                            }
                        });
                        switch (s)
                        {
                            case "Hello Server":
                                final JSONObject json1 = new JSONObject();

                                try {
                                    json1.put("RPM", serverClass.GetRpm());
                                    json1.put("Speed", serverClass.GetSpeed());
                                    json1.put("Pressure", serverClass.GetPressure());
                                } catch (JSONException e) {
                                    e.printStackTrace();
                                }
                                Log.d(TAG, "onRequest: send " +json1);

                                webSocket.send(String.valueOf(json1));
                                runOnUiThread(new Runnable() {

                                    @Override
                                    public void run() {
                                        tt = new TextView(getInstance());
                                        tt.setText(" Server > " +json1);
                                        Ll.addView(tt);
                                        ScrolLog.fullScroll(View.FOCUS_DOWN);
                                    }
                                });
                                break;
                        }
                    }
                });
            }
        });
        SERVER_IP = getIpAddress();
        tvIP.setText("IP: " + SERVER_IP);
        tvPort.setText("Port: " + SERVER_PORT);
// listen on port 5000
        server.listen(AsyncServer.getDefault(), SERVER_PORT);

// browsing http://localhost:5000 will return Hello!!!
        //endregion

    }


    public String getIpAddress() {
        String ip = "";
        int i =0;
        try {
            Enumeration<NetworkInterface> enumNetworkInterfaces = NetworkInterface
                    .getNetworkInterfaces();
            while (enumNetworkInterfaces.hasMoreElements()) {
                NetworkInterface networkInterface = enumNetworkInterfaces
                        .nextElement();
                Enumeration<InetAddress> enumInetAddress = networkInterface
                        .getInetAddresses();
                while (enumInetAddress.hasMoreElements()) {
                    InetAddress inetAddress = enumInetAddress
                            .nextElement();

                    if (inetAddress.isSiteLocalAddress()) {
                        if(i==0){
                            ip += inetAddress.getHostAddress();
                            i++;
                        }else{
                            ip += " and "+inetAddress.getHostAddress();
                        }

                    }
                }
            }
        } catch (SocketException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            ip += "Something Wrong! " + e.toString() + "\n";
        }
        return ip;
    }


    //Broadcast receiver des messages bluetooth reçu
    StringBuilder messages= new StringBuilder();
    BroadcastReceiver mReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            String text = intent.getStringExtra("theMessage");
            //Log.d(TAG, "onReceive : " +text);

                decode_resp(text);
                // correspondancePIDJSON(text);

            messages = new StringBuilder();
        }
    };

    //initialisation des gauges
    private void addTextGauge(Object print_type, String pidNumber, ArrayList listTextInde_or_gaugeInde, ArrayList listText_or_Gauge) {
        listTextInde_or_gaugeInde = new ArrayList<>();
        listTextInde_or_gaugeInde.add(pidNumber);
        listTextInde_or_gaugeInde.add(print_type);
        listText_or_Gauge.add(listTextInde_or_gaugeInde);
    }

    private static GaugeClass instance;

    public static GaugeClass getInstance() {
        return instance;
    }

//region menu et toolbar
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        if (id_view == 0) {
            getMenuInflater().inflate(R.menu.menu_main, menu);
        } else if (id_view == 1) {
            getMenuInflater().inflate(R.menu.menu_gauge, menu);
        }
        this.menu = menu;
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        if (id == R.id.action_add) {
            Log.d(TAG, "onOptionsItemSelected: hello tt le monde ");
            MyDialogModal();
        }

            return super.onOptionsItemSelected(item);
    }

    @Override
    public boolean onSupportNavigateUp() {
        id_view = 0;

        // a refaire ! avec un share parametre pour avoir les anciennes infos dans la premiere page
        //ainsi que passer la connection ble
        Intent myIntent = new Intent(GaugeClass.this, MainActivity.class);
        myIntent.putExtra("key", "hi"); //Optional parameters
        GaugeClass.this.startActivity(myIntent);
        return true;
    }
    //endregion

    //region MODAL et Création de nouveau text etc.
    TextView text_PID;
    ArrayList<ItemList> ListItem;
    String Description = "";
    String PID = "";

    public void MyDialogModal() {
        // ListPID();
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(GaugeClass.this);
        //list_dialog.setContentView(R.layout.list_dialog);

        View mView = getLayoutInflater().inflate(R.layout.dialogbox, null);

        mBuilder.setTitle("Add a new PID");

        text_PID = mView.findViewById(R.id.text_choixPID);
        mBuilder.setPositiveButton(R.string.but_dialog_pos, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int id) {
                dialog.cancel();
                creationNewLayout(Description,PID);
            }
        });
        mBuilder.setNegativeButton(R.string.but_dialog_neg, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int id) {
                dialog.cancel();
            }
        });

        ListItem = ListPID();
        ListView listView = mView.findViewById(R.id.lst);

        CustomAdapter adapter = new CustomAdapter(this, R.layout.listviewadapter, ListItem);
        listView.setAdapter(adapter);


        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                PID = ListItem.get(position).getText1();
                Description = ListItem.get(position).getText2();
                Log.d(TAG, "on click pair: " + PID + " : " + Description);

                text_PID.setText(PID + " : " + Description);

            }
        });

        mBuilder.setView(mView);

        AlertDialog dialog = mBuilder.create();
        dialog.show();
    }

    private void creationNewLayout(String title,String PID_list){
        if((number_add%3)==0){

            LinearLayout vert = new LinearLayout(this);
            LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT);
            float marg_start = getResources().getDimension(R.dimen.start_margin);
            float marg_end = getResources().getDimension(R.dimen.end_margin);
            params.setMarginStart((int) marg_start);
            params.setMarginEnd((int) marg_end);

            vert.setLayoutParams(params);
            vert.setOrientation(LinearLayout.HORIZONTAL);
            Tab_add_Text.add(vert);
            linearLayoutText.addView(vert);
        }
        creationNewText(title,PID_list);
    }

    private void creationNewText(String title,String PID_list) {

        LinearLayout horiz = new LinearLayout(this);
        horiz.setLayoutParams(new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MATCH_PARENT,1.0f));
        horiz.setOrientation(LinearLayout.VERTICAL);

        TextView tv1 = new TextView(this);
        TextView tv2 = new TextView(this);
        tv1.setGravity(Gravity.CENTER);
        tv2.setGravity(Gravity.CENTER);

        LinearLayout.LayoutParams params2 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT);
        float heigth = getResources().getDimension(R.dimen.heigth);
        LinearLayout.LayoutParams params1 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, (int) heigth);
        tv2.setLayoutParams(params2);
        tv1.setBackgroundResource(R.drawable.back);
        tv1.setText(title);
        tv2.setText("0");
        tv1.setLayoutParams(params1);

        LinearLayout e = Tab_add_Text.get(Tab_add_Text.size() - 1);

        e.addView(horiz);
        horiz.addView(tv1);
        horiz.addView(tv2);
        number_add++;
        addTextGauge(tv2, PID_list, listTextInde, listText);
       // mDecryptMessage.setListSprite(listSprite);
    }
    //endregion

//Lister les PIDs faire différente chose après comme les afficher pour rajouté un nouveau PID a requéter
    private ArrayList<ItemList> ListPID() {
        ArrayList<ItemList> mitemRecyclerViews = new ArrayList<>();
        List<String> mm = new ArrayList<>();
        try {
            InputStream inputStream = getAssets().open("PID.json");
            int size = inputStream.available();
            byte[] buffer = new byte[size];
            inputStream.read(buffer);
            inputStream.close();

            String myJson = new String(buffer, StandardCharsets.UTF_8);
            JSONObject jsonComplet = new JSONObject(myJson);
            JSONArray results = new JSONArray(jsonComplet.getString("Service 01"));
            final int numberOfItemsInResp = results.length();
            Log.d(TAG, "ListPID: " + numberOfItemsInResp);

            for (int i = 0; i < numberOfItemsInResp; i++) {

                JSONObject obj = new JSONObject(results.getString(i));
                mitemRecyclerViews.add(new ItemList(obj.getString("PID"), obj.getString("description")));
                mm.add(obj.getString("PID") + obj.getString("description"));

            }
            inputStream.close();

        } catch (Exception e) {
            e.printStackTrace();
            System.out.println(e);
        }

        for (ItemList dd : mitemRecyclerViews) {
            Log.d(TAG, "ListPID: " + dd.getText1());
            Log.d(TAG, "ListPID: " + dd.getText2());
        }

        return mitemRecyclerViews;
    }


//region RUN write // il faudrait que l'on puisse géré quand on implémente un nouveau PIDs etc..
    public void testmulti(View view) {
        if(!runningmulti){
            loop2();
        }else{
            runningmulti=false;
            text_sending_etat.setText("Stop");
        }

    }


    public void loop2() {
            runningmulti=true;
            d= new Thread(new Runnable() {

                public void run() {
                    // loop until the thread is interrupted
                    while (runningmulti) {
                        byte[] bytes = (envoie_chaine + "\r").getBytes(Charset.defaultCharset());
                        mBluetoothConnectionService.write(bytes, envoie_chaine);
                        try {
                            Thread.sleep(500);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
            });
            text_sending_etat.setText("Started");
            d.start();

    }


    //test
    public void loop() {
        new Thread(new Runnable() {
            public void run() {
                // loop until the thread is interrupted
                while (!Thread.currentThread().isInterrupted()) {
                    //byte[] bytes = ("01 0C 05 OD 33" + "\r").getBytes(Charset.defaultCharset());
                    // mBluetoothConnectionService.write(bytes, "01 0C 05 OD 33");5C 40 05 50 04 08

                    decode_resp("00A 0:41 0C 4F FF 0D 08 1:33 5F 46 02 00 00 00");
                    try {
                        Thread.sleep(2000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                   /* byte[] bytes = ("01 0C 05 OD 33" + "\r").getBytes(Charset.defaultCharset());
                    mBluetoothConnectionService.write(bytes, "01 0C 05 OD 33");*/

                    decode_resp("41 0C 00 FF 0D 80 33 51 46 80 5C 8F 05 F0 04 F8");

                    try {
                        Thread.sleep(1000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }
        }).start();
    }
    //endregion dss


    //region Decryptage et affichage des resultats gauges et text
    /*il faudrait gerer ça dans une classe pour pouvoir l'utiliser de n'importe ou
    Mais le problème c'est qu'il faut parler avec le server PID et les gauges avec cette classe ce
    qui pose un problème de construction de l'algorthme et de la classe*/

    private static Pattern WHITESPACE_PATTERN = Pattern.compile("\\s");

    protected String removeAll(Pattern pattern, String input) {
        return pattern.matcher(input).replaceAll("");
    }

    public void decode_resp(String text){
        String dd = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        Log.d(TAG, "decode_resp: "+text);
        if(dd.substring(0,6 ).equals("NODATA")){
            Toast.makeText(GaugeClass.getInstance(), "NO DATA", Toast.LENGTH_SHORT).show();
        }else if(dd.substring(0,8).equals("CANERROR")){
            Toast.makeText(GaugeClass.getInstance(), "CAN ERROR", Toast.LENGTH_SHORT).show();
        }
        else if(dd.substring(0,12).equals("SEARCHING...")){
            Toast.makeText(GaugeClass.getInstance(), "CAN ERROR", Toast.LENGTH_SHORT).show();
        }
        else {
            Log.d(TAG, "decode_resp: enter with "+ text);
            String PID = dd.substring(0, 2);
            String text_final = "";
            boolean flag_end = false;
            int longeur = 0;
            switch (PID) {
                case "41":
                    text_final = dd.substring(2);

                    Log.d(TAG, "decode_resp 41 : " + text_final);
                    longeur = 0;
                    flag_end = true;
                    break;

                default:
                    flag_end = false;
                    //multiple request
                    String multi_request = dd.substring(0, 3);
                    longeur = 2 * ((int) Long.parseLong(multi_request, 16));//2* fois la longeur car c'est en HEX
                    String text_mileu = dd.substring(3);
                    String[] text_mil = text_mileu.split(":");
                   // Log.d(TAG, "decode_resp: " + Arrays.toString(text_mil) + "  : " + longeur);
                    for (int k = 1; k < text_mil.length; k++) {
                        String str = "";
                        if (k == text_mil.length) {
                            str = text_mil[k];
                        } else {
                            str = text_mil[k].substring(0, text_mil[k].length() - 1);
                          //  Log.d(TAG, "decode_resp: str " + str);
                        }
                        text_final = text_final + str;
                    }
                    //attention à ce que la longeur ne soit pas plus importante que la longeur de la chaine !!
                    if (longeur <= text_final.length()) {
                        text_final = text_final.substring(0, longeur);
                        //Log.d(TAG, "decode_resp: " + text_final + "  : " + longeur);
                        decode_resp(text_final);
                    }else{
                        Toast.makeText(GaugeClass.getInstance(), "LENGTH >", Toast.LENGTH_SHORT).show();
                    }
                    break;
            }
            if (flag_end) {
                //Log.d(TAG, "decode_resp: " + text_final);
                correspondancePIDJSON(text_final);
            }
        }
    }

    private void correspondancePIDJSON(String text) {
        String text2 = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        Log.d(TAG, "correspondancePID: trim "+text2 + ": " + text2.length());

        while (text2.length() > 0) {
            //while str.length>0 Il faut savoir exactement le nombre d'octet que nous avons
            int i = 0;
            String iden = text2.substring(0,2);
            // Log.d(TAG, "correspondancePID: id "+iden);
            String data = text2.substring(2);
            // Log.d(TAG, "correspondancePID: dat "+data);


            // attendons en reception car dans les multiple PID request on receptionne tout en meme temp!!!!
            try {
                InputStream is = getAssets().open("PID.json");
                int size = is.available();
                byte[] buffer = new byte[size];
                is.read(buffer);
                is.close();
                Long finalResult;
                while (i <= size) {

                    //"41 0C 4F FF 0D 08 33 5F" exemple
                    String myJson = new String(buffer, "UTF-8");
                    JSONObject jsonComplet = new JSONObject(myJson);
                    JSONArray array = new JSONArray(jsonComplet.getString("Service 01"));//mettre une liste des services dispo et le choisir dans la fonctind'avant
                    JSONObject obj = new JSONObject(array.getString(i));

                    if (iden.equals(obj.getString("PID"))) {
                        text2 = data.substring(Integer.parseInt(obj.getString("nbBytes")));
                        Long valeur_sans_op = Long.parseLong(data.substring(0, Integer.parseInt(obj.getString("nbBytes"))), 16);
                        // Log.d(TAG, "correspondancePID: rpm traite " +valeur_sans_op);

                        switch (obj.getString("operations")) {
                            case "false":
                                finalResult = valeur_sans_op;
                               // Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
                                ServerPID(obj,finalResult);
                                gaugeText(obj, finalResult, listSprite);
                                break;

                            case "1":
                                finalResult = operation(1, valeur_sans_op, obj);
                               // Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
                                ServerPID(obj,finalResult);
                                gaugeText(obj, finalResult, listSprite);
                                break;

                            case "2":
                                finalResult = operation(2, valeur_sans_op, obj);
                               // Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
                                ServerPID(obj,finalResult);
                                gaugeText(obj, finalResult, listSprite);
                            break;
                        }
                        break;
                    } else {
                        //Toast.makeText(MainActivity.this,text2 , Toast.LENGTH_SHORT).show();
                        text2 = text2.substring(1);
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
        int k = 1;
        Long value = valeur_sans_op;
        while (k <= i) {
            switch (ob.getString("operateur" + k)) {
                case "/":
                    value = value / Long.parseLong(ob.getString("value" + k));
                    break;
                case "-":
                    value = value - Long.parseLong(ob.getString("value" + k));
                    break;
                case "+":
                    value = value + Long.parseLong(ob.getString("value" + k));
                    break;
                case "*":
                    value = value * Long.parseLong(ob.getString("value" + k));
                    break;
            }
            k++;
        }
        return value;
    }

    private void gaugeText(JSONObject object, final Long value, List<ArrayList<ArrayList<Object>>> listPrint) throws JSONException {
       // Log.d(TAG, "gaugeText 1 : " + listPrint);
        for (int o = 0; o <= listPrint.size(); o = o + 2) {
            if (object.getString("print").equals("gauge")) {

             //   Log.d(TAG, "gaugeText 2 : " + listPrint.get(0));
                for (int i = 0; i <= listPrint.get(0).size(); i++) {
                   // Log.d(TAG, "gaugeText 3 : " + listPrint.get(0).get(i));
                    if (listPrint.get(0).get(i).get(0).equals(object.getString("PID"))) {
                      //  Log.d(TAG, "gaugeText: hello herre");
                        final Gauge d = (Gauge) listPrint.get(0).get(i).get(1);

                        Needle.onMainThread().execute(new Runnable() {

                            @Override
                            public void run() {
                                d.moveToValue(value);
                            }
                        });
                        break;
                    }
                }
            } else if (object.getString("print").equals("text")) {
               // Log.d(TAG, "Text 2 : " + listPrint.get(1));
                for (int i = 0; i <= listPrint.get(1).size(); i++) {
                  //  Log.d(TAG, "Text 3 : " + listPrint.get(1).get(i));
                    if (listPrint.get(1).get(i).get(0).equals(object.getString("PID"))) {
                       // Log.d(TAG, "Text: hello here");
                        final TextView d = (TextView) listPrint.get(1).get(i).get(1);

                        Needle.onMainThread().execute(new Runnable() {
                            @Override
                            public void run() {
                                d.setText(String.valueOf(value));
                            }
                        });
                        break;
                    }
                }


            }
        }
    }

    //endregion


    private void ServerPID(JSONObject objet,Long res) throws JSONException {
        Log.d(TAG, "ServerPID: "+objet.getString("PID")+ "number "+res);
        switch (objet.getString("PID")){
            case "0D":
                // Log.e(TAG, "ServerPID: entrer vitesse" );
                serverClass.SetSpeed(res);
                break;
            case "0C":
                // Log.e(TAG, "ServerPID: entrer RPm" );
                serverClass.SetRpm(res);
                break;
            case "33":
                serverClass.SetPressure(res);
                break;
            default:
                //  Log.d(TAG, "ServerPID: fail " + objet.getString("PID"));
                break;
        }

    }



}
