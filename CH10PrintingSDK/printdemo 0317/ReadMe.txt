本源码为调用打印服务的调用示例，打印服务本质上是一个Service组件，通过bindService API与之建立连接，连接建立后便可发送Epson指令进行打印。

This project is a demo that show how to call printer service,the printer service is a Android Service Component.
According to the bindService API,your application can connect with the printer service.Once you have connected 
with the printer service,you can send the Epson command to print.


使用说明：
1.将AIDL文件 PrinterInterface.aidl 添加到包 recieptservice.com.recieptservice 中。
2.用如下代码绑定打印服务
Intent intent=new Intent();
intent.setClassName("recieptservice.com.recieptservice","recieptservice.com.recieptservice.service.PrinterService");
bindService(intent, new ServiceConnection(),Service.BIND_AUTO_CREATE);
3.绑定成功后即可开始调用相关API接口进行打印。



AIDL文档说明：

Epson指令打印接口：
void printEpson(in byte []data);

获取打印服务版本号：
String getServiceVersion();

打印String文本：
void printText(String text);

打印BitMap图片：
void printBitmap(in Bitmap pic);

打印条码：
data：条码内容
symbology： 条码类型
    0 -- UPC-A
    1 -- UPC-E
    2 -- JAN13(EAN13)
    3 -- JAN8(EAN8)
    4 -- CODE39
    5 -- ITF
    6 -- CODABAR
    7 -- CODE93
    8 -- CODE128
height: 		条码高度 1-255  默认162
width: 		条码宽度 2-6, 默认2
void printBarCode(String data, int symbology, int height, int width);

打印二维码：
data：条码内容
modulesize ：二维码块大小  1-16 
errorlevel ：二维码纠错等级 0-3
void printQRCode(String data, int modulesize, int errorlevel);

对齐方向：
alignment 0 居左  1 居中 2 居右
void setAlignment(int alignment);

设定字体大小：
void setTextSize(float textSize);

换N行：
void nextLine(int line);

打印表格：
Text 表格内容
weight 行宽度权重
alignment 每行对齐方向
void printTableText(in String[] text,in int []weight,in int []alignment);

设置字体加粗：
void setTextBold(boolean bold);



