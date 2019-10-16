package com.evanblondeau.tuto_bl;

import android.bluetooth.BluetoothAdapter;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.support.v4.content.ContextCompat;
import android.util.Log;


/*broadcast receiver à finir pour qu'il gére tous les messages qui arrive de l'appareil, pour le
bluetooth, le server etc..*/
public class BroadReceiver extends BroadcastReceiver {


    private static final String TAG = "broad";

    @Override
    public void onReceive(Context context, Intent intent) {
        String action = intent.getAction();
        if (action.equals(BluetoothAdapter.ACTION_STATE_CHANGED)) {
            final int state = intent.getIntExtra(BluetoothAdapter.EXTRA_STATE, BluetoothAdapter.ERROR);
            switch (state) {
                case BluetoothAdapter.STATE_OFF:
                    Log.d(TAG, "OnReceive: STATE OFF");
                    /*enableBtnCon(false);
                    enableBtnSend(false);
                    menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.ic_bluetooth_disabled_black_24dp));*/
                    break;
                case BluetoothAdapter.STATE_TURNING_OFF:
                    Log.d(TAG, "OnReceive: STATE_TURNING_OFF");
                    break;
                case BluetoothAdapter.STATE_ON:
                    Log.d(TAG, "OnReceive: STATE_ON");
                    /*menu.getItem(2).setIcon(ContextCompat.getDrawable(context, R.drawable.ic_bluetooth_black_24dp));
                    enableDiscoverable();
                    pairedDevices(context);*/
                    break;
                case BluetoothAdapter.STATE_TURNING_ON:
                    Log.d(TAG, "OnReceive: STATE_TURNING_ON");
                    break;

            }
        }
    }
}
