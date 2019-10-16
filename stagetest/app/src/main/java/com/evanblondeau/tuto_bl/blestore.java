package com.evanblondeau.tuto_bl;

import android.app.Application;

// permet de faire persister la connection bluetooth a travers les activités
public class blestore extends Application {

    public BluetoothConnectionService myBlueComms;

    @Override
    public void onCreate()
    {
        super.onCreate();
        sInstance = this;
    }

    public void setMyBlueComms(BluetoothConnectionService ble)
    {
        myBlueComms = ble;
    }

    public BluetoothConnectionService getMyBlueComms(){
        return myBlueComms;
    }

    public static blestore sInstance;

    public static blestore getInstance(){
        return sInstance;
    }
}
