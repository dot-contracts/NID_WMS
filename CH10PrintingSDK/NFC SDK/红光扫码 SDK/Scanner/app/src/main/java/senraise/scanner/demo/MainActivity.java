package senraise.scanner.demo;

import androidx.appcompat.app.AppCompatActivity;
import android.content.BroadcastReceiver;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.Context;

import android.os.Bundle;
import android.util.Log;

public class MainActivity extends AppCompatActivity {

    MyReceiver myReceiver=new MyReceiver();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        registerReceiver(myReceiver,new IntentFilter("android.scanner.scan"));
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(myReceiver);
    }
}
