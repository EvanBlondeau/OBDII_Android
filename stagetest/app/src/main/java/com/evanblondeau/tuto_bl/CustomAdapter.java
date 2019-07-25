package com.evanblondeau.tuto_bl;

import android.content.Context;
import android.nfc.Tag;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.RadioButton;
import android.widget.TextView;

import java.util.ArrayList;

public class CustomAdapter extends ArrayAdapter<ItemList> {

    private static final String TAG = "adapter";
    private Context mContext;
    int mRessource;

    public CustomAdapter(Context context, int resource, ArrayList<ItemList> obj) {
        super(context, resource, obj);
        mContext=context;
        mRessource=resource;
    }

    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        String PID = getItem(position).getText1();
        String Description = getItem(position).getText2();

        ItemList it = new ItemList(PID,Description);

        LayoutInflater inflater = LayoutInflater.from(mContext);
        convertView=inflater.inflate(mRessource,parent,false);

        TextView textView = convertView.findViewById(R.id.textPID);
        TextView textView2 = convertView.findViewById(R.id.textDescript);
        RadioButton rB=convertView.findViewById(R.id.radioButton);

        textView.setText(PID);
        textView2.setText(Description);
        return convertView;
    }

}
