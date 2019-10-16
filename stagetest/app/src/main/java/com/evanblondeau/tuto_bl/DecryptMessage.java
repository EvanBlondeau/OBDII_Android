package com.evanblondeau.tuto_bl;

import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.InputStream;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.regex.Pattern;

import de.nitri.gauge.Gauge;
import needle.Needle;


//Class a finir pour gerer tout le decryptage des messages  PIDs
public class DecryptMessage {

    private static final String TAG = "DecryptMessage";
    public transient InputStream inputStream;

    ArrayList<ArrayList<ArrayList<Object>>> listSprite = new ArrayList<>();

    public DecryptMessage(InputStream inputStream2)
    {
        inputStream = inputStream2;
    }

    public void setListSprite( ArrayList<ArrayList<ArrayList<Object>>> list){
        listSprite = list;
    }


    private static Pattern WHITESPACE_PATTERN = Pattern.compile("\\s");

    protected String removeAll(Pattern pattern, String input) {
        return pattern.matcher(input).replaceAll("");
    }

    public void decode_resp(String text){
        String dd = removeAll(WHITESPACE_PATTERN, text); //removes all [ \t\n\x0B\f\r]
        if(dd.substring(0,6 ).equals("NODATA")){
            Toast.makeText(GaugeClass.getInstance(), "NO DATA", Toast.LENGTH_SHORT).show();
        }else if(dd.substring(0,8).equals("CANERROR")){
            Toast.makeText(GaugeClass.getInstance(), "CAN ERROR", Toast.LENGTH_SHORT).show();
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
                        Toast.makeText(GaugeClass.getInstance(), "LENGTH >", Toast.LENGTH_SHORT).show();
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
                InputStream is = inputStream;/// ??????????????????????????????????????????????????,
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
                Log.d(TAG, "Text 2 : " + listPrint.get(1));
                for (int i = 0; i <= listPrint.get(1).size(); i++) {
                    Log.d(TAG, "Text 3 : " + listPrint.get(1).get(i));
                    if (listPrint.get(1).get(i).get(0).equals(object.getString("PID"))) {
                        Log.d(TAG, "Text: hello here");
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
}
