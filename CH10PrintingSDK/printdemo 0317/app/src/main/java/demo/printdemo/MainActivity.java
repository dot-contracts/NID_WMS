package demo.printdemo;

import android.app.Service;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.graphics.BitmapFactory;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

import recieptservice.com.recieptservice.PrinterInterface;

public class MainActivity extends AppCompatActivity {
    int textSize=24;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Intent intent=new Intent();
        intent.setClassName("recieptservice.com.recieptservice","recieptservice.com.recieptservice.service.PrinterService");
        bindService(intent, new ServiceConnection() {
            @Override
            public void onServiceConnected(ComponentName name, final IBinder service) {
                MainActivity.this.findViewById(R.id.test2).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                                mAidl.printText(((EditText)findViewById(R.id.test1)).getText().toString()+"\n");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test3).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printBitmap(BitmapFactory.decodeResource(getResources(),R.mipmap.ic_launcher));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test4).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printQRCode("123456",4,3);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test5).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.print128BarCode("12345678901234567890123", 3, 80, 2);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test6).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.setTextBold(true);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test7).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.setTextSize(--textSize);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test12).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.setTextSize(++textSize);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test8).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printText("\n");
                            mAidl.setAlignment(0);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test9).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printText("\n");
                            mAidl.setAlignment(1);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test10).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printText("\n");
                            mAidl.setAlignment(2);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test11).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.nextLine(1);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                MainActivity.this.findViewById(R.id.test13).setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        PrinterInterface mAidl = PrinterInterface.Stub.asInterface(service);
                        try {
                            mAidl.printTableText(new String[]{"宫保鸡丁","一份","20元"},new int[]{1,1,1},new int[]{1,1,1});
                            mAidl.printTableText(new String[]{"鸡丁","一份","10元"},new int[]{1,1,1},new int[]{1,1,1});
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }

            @Override
            public void onServiceDisconnected(ComponentName name) {

            }
        }, Service.BIND_AUTO_CREATE);
    }
}
