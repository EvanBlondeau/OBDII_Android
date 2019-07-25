package com.evanblondeau.tuto_bl;

import android.Manifest;
import android.app.Dialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.Layout;
import android.util.Log;
import android.util.TypedValue;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
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
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;
import java.util.Set;
import java.util.UUID;
import java.util.regex.Pattern;

import de.nitri.gauge.Gauge;

public class MainActivity extends AppCompatActivity {

//region Global variable
    private static final String TAG = "Main Activity";
    int REQUEST_ENABLE_BT = 1;
    BluetoothAdapter mBluetoothAdapter;

    private static MainActivity instance;
    Button buttonessai;
    BluetoothConnectionService mBluetoothConnectionService;

    private static final UUID MY_UUID = UUID.fromString("00001800-0000-1000-8000-00805f9b34fb");
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
    Context mContext;
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
    char id_view = 0;

    private Menu menu;

    public ArrayList<BluetoothDevice> mBTDevice = new ArrayList<>();
    public ArrayList<BluetoothDevice> mBTPairedDevice = new ArrayList<>();
    public DeviceListAdapter mDeviceListAdapter;

    ListView listDevice;
    ListView listPairedDevice;

    ArrayList<ArrayList<ArrayList<Object>>> listSprite = new ArrayList<>();
    ArrayList<ArrayList<Object>> listGauge = new ArrayList<>();
    ArrayList<ArrayList<Object>> listText = new ArrayList<>();
    ArrayList<Object> listGaugeInde = new ArrayList<>();
    ArrayList<Object> listTextInde = new ArrayList<>();

    LinearLayout linearLayoutText;
    ArrayList<LinearLayout> Tab_add_Text = new ArrayList<>();
    int number_add;
//endregion

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // btnEnableDisable_discoverable = findViewById(R.id.btn_enbale_disco);
        listDevice = findViewById(R.id.list);
        listPairedDevice = findViewById(R.id.listPaired);
        mBTDevice = new ArrayList<>();
        mBTPairedDevice = new ArrayList<>();

        instance = this;
        mContext = this;
        Toolbar toolbar = findViewById(R.id.toolbar);

        setSupportActionBar(toolbar);
        InString = findViewById(R.id.LinearIn);
        progressBar = findViewById(R.id.progressBar);
        progressBar.setProgress(0);
        progressBar.setProgressTintList(ColorStateList.valueOf(Color.RED));

        scrollOut = findViewById(R.id.LinearOutScr);
        scrollIn = findViewById(R.id.LinearInScr);

        text_id = findViewById(R.id.textid);
        text_con_state = findViewById(R.id.text_connect);

        LocalBroadcastManager.getInstance(this).registerReceiver(mReceiver, new IntentFilter("incomingMessage"));
        text_test = findViewById(R.id.texttest);
        messages = new StringBuilder();


        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        btnStartConnection = findViewById(R.id.Start);
        btnSend = findViewById(R.id.btnSend);
        editText = findViewById(R.id.Edit);

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
                String str = editText.getText().toString();
                strIn = new TextView(context);
                String str2 = " > " + str;
                strIn.setText(str2);
                InString.addView(strIn);

                byte[] bytes = (str + "\r").getBytes(Charset.defaultCharset());
                mBluetoothConnectionService.write(bytes, str);
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
                Log.d(TAG, "on click pair: " + deviceName + " : " + deviceAdress);

                //create appairage
                if (Build.VERSION.SDK_INT > Build.VERSION_CODES.JELLY_BEAN_MR2) {
                    Log.d(TAG, "Trying to pair with " + deviceName);
                    if (mBTDevice.get(position).getBondState() == 12) {
                        Toast.makeText(MainActivity.this, "Already bounded to " + deviceName, Toast.LENGTH_SHORT).show();
                        text_id.setText(deviceName);
                        enableBtnCon(true);
                    } else {
                        mBTDevice.get(position).createBond();
                    }
                    mBluetoothDevice = mBTDevice.get(position);
                    mBluetoothConnectionService = new BluetoothConnectionService((MainActivity.this));

                }
            }
        });

        listPairedDevice.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                String deviceName = mBTPairedDevice.get(position).getName();
                String deviceAdress = mBTPairedDevice.get(position).getAddress();
                Log.d(TAG, "on click already paired: " + deviceName + " : " + deviceAdress);

                Toast.makeText(MainActivity.this, "Bounded to " + deviceName, Toast.LENGTH_SHORT).show();
                text_id.setText(deviceName);

                mBluetoothConnectionService = new BluetoothConnectionService((MainActivity.this));
                mBluetoothDevice = mBTPairedDevice.get(position);
                enableBtnCon(true);
            }
        });
        IntentFilter filterpair = new IntentFilter(BluetoothDevice.ACTION_BOND_STATE_CHANGED);
        registerReceiver(mBroadcastReceiver4, filterpair);

        pairedDevices(this);
        enableBtnCon(false);
        enableBtnSend(false);
        number_add=0;
    }

    public void enableBtnCon(boolean val) {
        if (val) {
            btnStartConnection.setEnabled(true);
            Log.d(TAG, "enableBtnSend: enable");
        } else {
            btnStartConnection.setEnabled(false);
            Log.d(TAG, "enableBtnCon: disable");
        }
    }

    public void enableBtnSend(boolean val) {
        if (val) {
            btnSend.setEnabled(true);
            Log.d(TAG, "enableBtnSend: enable");
        } else {
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
                final int state = intent.getIntExtra(BluetoothAdapter.EXTRA_STATE, mBluetoothAdapter.ERROR);
                switch (state) {
                    case BluetoothAdapter.STATE_OFF:
                        Log.d(TAG, "OnReceive: STATE OFF");
                        enableBtnCon(false);
                        enableBtnSend(false);
                        menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.ic_bluetooth_disabled_black_24dp));
                        break;
                    case BluetoothAdapter.STATE_TURNING_OFF:
                        Log.d(TAG, "OnReceive: STATE_TURNING_OFF");
                        break;
                    case BluetoothAdapter.STATE_ON:
                        Log.d(TAG, "OnReceive: STATE_ON");
                        menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.ic_bluetooth_black_24dp));
                        enableDiscoverable();
                        pairedDevices(context);
                        break;
                    case BluetoothAdapter.STATE_TURNING_ON:
                        Log.d(TAG, "OnReceive: STATE_TURNING_ON");
                        break;

                }
            }
        }
    };

    // Create a BroadcastReceiver for ACTION CHANGE BLE.
    private final BroadcastReceiver mBroadcastReceiver2 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            assert action != null;
            if (action.equals(mBluetoothAdapter.ACTION_SCAN_MODE_CHANGED)) {
                int mode = intent.getIntExtra(BluetoothAdapter.EXTRA_SCAN_MODE, mBluetoothAdapter.ERROR);
                switch (mode) {
                    case BluetoothAdapter.SCAN_MODE_CONNECTABLE:
                        Log.d(TAG, "mBroadcastReceiver2: Discorability Disable. Able to receive connections");

                        break;
                    case BluetoothAdapter.SCAN_MODE_NONE:
                        Log.d(TAG, "mBroadcastReceiver2: Discoverability Disable. Nor able to receive connection");
                        break;
                    case BluetoothAdapter.STATE_CONNECTING:
                        Log.d(TAG, "mBroadcastReceiver2: Connecting");
                        break;
                    case BluetoothAdapter.STATE_CONNECTED:
                        Log.d(TAG, "mBroadcastReceiver2: Connected ");
                        break;
                }
            }
        }
    };

    // Create a BroadcastReceiver for ACTION PAIRED.
    private final BroadcastReceiver mBroadcastReceiver4 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            final String action = intent.getAction();

            assert action != null;
            if (action.equals(BluetoothDevice.ACTION_BOND_STATE_CHANGED)) {
                BluetoothDevice mDevice = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                // paired
                if (mDevice.getBondState() == BluetoothDevice.BOND_BONDED) {
                    Log.d(TAG, "mBroadcastReceiver4: BOND_BOUNDED");
                    mBluetoothDevice = mDevice;
                    Toast.makeText(context, "Device bounded", Toast.LENGTH_SHORT).show();
                    text_id.setText(mDevice.getName());
                    enableBtnCon(true);
                }
                //creating a bound(pair)
                if (mDevice.getBondState() == BluetoothDevice.BOND_BONDING) {
                    Log.d(TAG, "mBroadcastReceiver4 : BOND_BOUNDING");
                    Toast.makeText(context, "Pairing to " + mDevice.getName(), Toast.LENGTH_SHORT).show();
                }
                //break a bound
                if (mDevice.getBondState() == BluetoothDevice.BOND_NONE) {
                    Log.d(TAG, "mBroadcastReceiver4 : BOND_NONE");
                }
            }
        }
    };

    @Override
    protected void onDestroy() {
        Log.d(TAG, "onDestroy Broadcast");
        super.onDestroy();
        unregisterReceiver(mBroadcastReceiver);
        unregisterReceiver(mBroadcastReceiver2);
        unregisterReceiver(mBroadcastReceiver3);
        unregisterReceiver(mBroadcastReceiver4);

    }

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

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_BLE) {
            Log.d(TAG, "OnClick: enabledisable BLE");
            enableDisableBT();
            return true;
        } else if (id == R.id.action_Disco) {
            disco_Device();
        } else if (id == R.id.action_Gauge) {

            listSprite = new ArrayList<>();

            id_view = 1;
            setContentView(R.layout.activity_sprite);
            Toolbar toolbar = findViewById(R.id.toolbar);

            linearLayoutText = findViewById(R.id.layout_text);

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

            listSprite.add(listText);

            //  ListPID();

        } else if (id == R.id.action_add) {
            Log.d(TAG, "onOptionsItemSelected: hello tt le monde ");
            MyDialogModal();
        }

        return super.onOptionsItemSelected(item);
    }

    private void addTextGauge(Object print_type, String pidNumber, ArrayList listTextInde_or_gaugeInde, ArrayList listText_or_Gauge) {
        listTextInde_or_gaugeInde = new ArrayList<>();
        listTextInde_or_gaugeInde.add(pidNumber);
        listTextInde_or_gaugeInde.add(print_type);
        listText_or_Gauge.add(listTextInde_or_gaugeInde);
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
            //Log.d(TAG, "onReceive : " +text);
            if (id_view == 0) {

                OutString = findViewById(R.id.LinearOut);
                messages.append(" > ").append(text);
                textOut = new TextView(context);
                textOut.setText(messages);
                OutString.addView(textOut);
                scrollOut.fullScroll(View.FOCUS_DOWN);
            } else if (id_view == 1) {
                decode_resp(text);
               // correspondancePIDJSON(text);
            }
            messages = new StringBuilder();
        }
    };


    //created method for starting connection
    //need to be paired first
    public void startConnection() {
        setBtnStartConnection(mBluetoothDevice, MY_UUID);
    }

    public void setBtnStartConnection(BluetoothDevice device, UUID uuid) {
        Log.d(TAG, "setBtnStartConnection: Initialising RFCOM Ble connection");
        progressBar.setProgress(25);
        mBluetoothConnectionService.startClient(device, uuid);
    }

    private void enableDisableBT() {
        if (mBluetoothAdapter == null) {
            Log.d(TAG, "you dont have bluetooth capabilities");
        }
        if (!mBluetoothAdapter.isEnabled()) {
            Intent enableBTIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
            startActivityForResult(enableBTIntent, REQUEST_ENABLE_BT);

            IntentFilter BTIntent = new IntentFilter(BluetoothAdapter.ACTION_STATE_CHANGED);
            registerReceiver(mBroadcastReceiver, BTIntent);
        }
        if (mBluetoothAdapter.isEnabled()) {
            mBluetoothAdapter.disable();

            IntentFilter BTIntent = new IntentFilter(BluetoothAdapter.ACTION_STATE_CHANGED);
            registerReceiver(mBroadcastReceiver, BTIntent);
        }
    }

    public void enableDiscoverable() {
        Log.d(TAG, "btn disco : making device disco for 300 seconds");
        Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
        discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 300);
        startActivity(discoverableIntent);

        IntentFilter intentFilter = new IntentFilter(mBluetoothAdapter.ACTION_SCAN_MODE_CHANGED);
        registerReceiver(mBroadcastReceiver2, intentFilter);
    }

    public void disco_Device() {
        Log.d(TAG, "btn discover : Looking for unpaired devices");
        if (mBluetoothAdapter.isDiscovering()) {
            mBluetoothAdapter.cancelDiscovery();
            Log.d(TAG, "btn discover : cancel discovery");
            //check permission
            checkBTpermission();
            mBluetoothAdapter.startDiscovery();
            IntentFilter discoverDeviceIntent = new IntentFilter(BluetoothDevice.ACTION_FOUND);
            registerReceiver(mBroadcastReceiver3, discoverDeviceIntent);
        }
        if (!mBluetoothAdapter.isDiscovering()) {
            //check permission
            checkBTpermission();
            mBluetoothAdapter.startDiscovery();
            IntentFilter discoverDeviceIntent = new IntentFilter(BluetoothDevice.ACTION_FOUND);
            registerReceiver(mBroadcastReceiver3, discoverDeviceIntent);
        }
        Toast.makeText(this, "Discover on", Toast.LENGTH_SHORT).show();
    }

    // Create a BroadcastReceiver for ACTION CHANGE BLE.
    private final BroadcastReceiver mBroadcastReceiver3 = new BroadcastReceiver() {
        public void onReceive(Context context, Intent intent) {
            final String action = intent.getAction();
            Log.d(TAG, "onReceive: action found mb3");

            if (action.equals(BluetoothDevice.ACTION_FOUND)) {
                BluetoothDevice device = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                mBTDevice.add(device);
                Log.d(TAG, "on receive" + device.getName() + ": " + device.getAddress());
                mDeviceListAdapter = new DeviceListAdapter(context, R.layout.device_adapter_view, mBTDevice);
                listDevice.setAdapter(mDeviceListAdapter);
            }
        }
    };

    /**
     * This method is required for all devices running API23+
     * Android must programmatically check the permissions for bluetooth. Putting the proper permissions
     * in the manifest is not enough.
     */
    private void checkBTpermission() {
        if (Build.VERSION.SDK_INT > Build.VERSION_CODES.LOLLIPOP) {
            int permissionCheck = this.checkSelfPermission("Manifest.permission.ACCESS_FINE_LOCATION");
            permissionCheck += this.checkSelfPermission("Manifest.permission.ACCESS_COARSE_LOCATION");
            if (permissionCheck != 0) {

                this.requestPermissions(new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 1001); //Any number
            }
        } else {
            Log.d(TAG, "checkBTPermissions: No need to check permissions. SDK version < LOLLIPOP.");
        }
    }

    public void changeProgressBar(int value, String color) {
        progressBar.setProgress(value);
        if (color.equals("GREEN")) {
            progressBar.setProgressTintList(ColorStateList.valueOf(Color.GREEN));
        } else {
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
            mDeviceListAdapter = new DeviceListAdapter(context, R.layout.device_adapter_view, mBTPairedDevice);
            listPairedDevice.setAdapter(mDeviceListAdapter);

        }
    }

    public void changeConnectionState(String value) {
        if (value.equals("Connected")) {
            text_con_state.setText("Connected");
        } else {
            text_con_state.setText("Disconnected");
        }
    }

    private static Pattern WHITESPACE_PATTERN = Pattern.compile("\\s");

    protected String removeAll(Pattern pattern, String input) {
        return pattern.matcher(input).replaceAll("");
    }

    private void decode_resp(String text){
        String dd = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        if(dd.substring(0,6 ).equals("NODATA")){
            Toast.makeText(MainActivity.this, "NO DATA", Toast.LENGTH_SHORT).show();
        }else if(dd.substring(0,8).equals("CANERROR")){
            Toast.makeText(MainActivity.this, "CAN ERROR", Toast.LENGTH_SHORT).show();
        }
        else {
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
                    Log.d(TAG, "decode_resp: " + Arrays.toString(text_mil) + "  : " + longeur);
                    for (int k = 1; k < text_mil.length; k++) {
                        String str = "";
                        if (k == text_mil.length) {
                            str = text_mil[k];
                        } else {
                            str = text_mil[k].substring(0, text_mil[k].length() - 1);
                            Log.d(TAG, "decode_resp: str " + str);
                        }
                        text_final = text_final + str;
                    }
                    //attention à ce que la longeur ne soit pas plus importante que la longeur de la chaine !!
                    if (longeur <= text_final.length()) {
                        text_final = text_final.substring(0, longeur);
                        Log.d(TAG, "decode_resp: " + text_final + "  : " + longeur);
                        decode_resp(text_final);
                    }else{
                        Toast.makeText(MainActivity.this, "LENGTH >", Toast.LENGTH_SHORT).show();
                    }
                    break;
            }
            if (flag_end) {
                Log.d(TAG, "decode_resp: " + text_final);
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


            // attendons en rececption car dans les multiple PID request on receptionne tout en meme temp!!!!
            try {
                InputStream is = getAssets().open("PID.json");
                int size = is.available();
                byte[] buffer = new byte[size];
                is.read(buffer);
                is.close();

                while (i <= size) {

                    //"41 0C 4F FF 0D 08 33 5F"
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
                                Long finalResult = valeur_sans_op;
                                Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
                                gaugeText(obj, finalResult, listSprite);
                                break;

                            case "1":
                                finalResult = operation(1, valeur_sans_op, obj);
                                Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
                                gaugeText(obj, finalResult, listSprite);
                                break;

                            case "2":
                                finalResult = operation(2, valeur_sans_op, obj);
                                Log.d(TAG, obj.getString("description") + " : " + finalResult + " " + obj.getString("unite"));
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
        Log.d(TAG, "gaugeText 1 : " + listPrint);
        for (int o = 0; o <= listPrint.size(); o = o + 2) {
            if (object.getString("print").equals("gauge")) {

                Log.d(TAG, "gaugeText 2 : " + listPrint.get(0));
                for (int i = 0; i <= listPrint.get(0).size(); i++) {
                    Log.d(TAG, "gaugeText 3 : " + listPrint.get(0).get(i));
                    if (listPrint.get(0).get(i).get(0).equals(object.getString("PID"))) {
                        Log.d(TAG, "gaugeText: hello herre");
                        final Gauge d = (Gauge) listPrint.get(0).get(i).get(1);

                        runOnUiThread(new Runnable() {

                            @Override
                            public void run() {
                                d.moveToValue(value);
                            }
                        });
                        break;
                    }
                }
            } else if (object.getString("print").equals("text")) {
                Log.d(TAG, "Text 2 : " + listPrint.get(1));
                for (int i = 0; i <= listPrint.get(1).size(); i++) {
                    Log.d(TAG, "Text 3 : " + listPrint.get(1).get(i));
                    if (listPrint.get(1).get(i).get(0).equals(object.getString("PID"))) {
                        Log.d(TAG, "Text: hello here");
                        final TextView d = (TextView) listPrint.get(1).get(i).get(1);

                        runOnUiThread(new Runnable() {
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

    public void testbutton(View view) {
        Log.d(TAG, "onClick: dd");
        loop();
    }

    public void test(View view) {
        if(!running){
            loop2(1);
        }else{
            running=false;
        }

    }

    public void testmulti(View view) {

        if(!runningmulti){
            loop2(2);
        }else{
            runningmulti=false;
        }

    }
    Thread t;
    Thread d;
    boolean running=false;
    boolean runningmulti=false;
    public void loop2(int var) {
        if(var==1){
            running=true;
            t=  new Thread(new Runnable() {
                public void run() {
                    // loop until the thread is interrupted
                    while (running) {
                        byte[] bytes = ("01 0C" + "\r").getBytes(Charset.defaultCharset());

                        mBluetoothConnectionService.write(bytes, "01 0C");
                        try {
                            Thread.sleep(500);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
            });
            t.start();
        }else if(var ==2){
            runningmulti=true;
           d= new Thread(new Runnable() {

                public void run() {
                    // loop until the thread is interrupted
                    while (runningmulti) {
                        byte[] bytes = ("01 0C 05 04 0D 33" + "\r").getBytes(Charset.defaultCharset());
                        mBluetoothConnectionService.write(bytes, "01 0C 05 04 0D 33");
                        try {
                            Thread.sleep(500);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
            });
           d.start();
        }

    }


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

    TextView text_PID;
    ArrayList<ItemList> ListItem;
    String Description = "";
    String PID = "";

    public void MyDialogModal() {
        // ListPID();
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(MainActivity.this);
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
    }

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


}
