package com.evanblondeau.tuto_bl;

import android.util.Log;

public class PIDServerClass {

    private Long speed;
    private Long rpm;
    private Long athmosphericPressure;

    public PIDServerClass(Long vit, Long regime_moteur, Long press){
        speed=vit;
        rpm=regime_moteur;
        athmosphericPressure=press;
    }

    public void SetSpeed(Long vitesse){
        Log.d("hello", "SetSpeed: " +vitesse);
        speed=vitesse;
    }

    public Long GetSpeed(){

        return speed;
    }

    public void SetRpm(Long moteur){
        Log.d("hello", "SetRpm: " +moteur);
        rpm=moteur;
    }

    public Long GetRpm(){
        return rpm;
    }

    public void SetPressure(Long pression){
        Log.d("hello", "SetPressure: " +pression);
        athmosphericPressure=pression;
    }

    public Long GetPressure(){

        return athmosphericPressure;
    }
}
