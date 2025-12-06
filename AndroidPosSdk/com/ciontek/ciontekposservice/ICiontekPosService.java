/*
 * This file is auto-generated.  DO NOT MODIFY.
 * Original file: /disk2/src/adm450_2018_2_0_amss/sdm450_2018_2_0_amss/LA.UM.8.6.2/LINUX/android/kbproject/samples/sdk/PosDemoCs32/app/src/main/aidl/com/ciontek/ciontekposservice/ICiontekPosService.aidl
 */
package com.ciontek.ciontekposservice;
// Declare any non-default types here with import statements

public interface ICiontekPosService extends android.os.IInterface
{
/** Local-side IPC implementation stub class. */
public static abstract class Stub extends android.os.Binder implements com.ciontek.ciontekposservice.ICiontekPosService
{
private static final java.lang.String DESCRIPTOR = "com.ciontek.ciontekposservice.ICiontekPosService";
/** Construct the stub at attach it to the interface. */
public Stub()
{
this.attachInterface(this, DESCRIPTOR);
}
/**
 * Cast an IBinder object into an com.ciontek.ciontekposservice.ICiontekPosService interface,
 * generating a proxy if needed.
 */
public static com.ciontek.ciontekposservice.ICiontekPosService asInterface(android.os.IBinder obj)
{
if ((obj==null)) {
return null;
}
android.os.IInterface iin = obj.queryLocalInterface(DESCRIPTOR);
if (((iin!=null)&&(iin instanceof com.ciontek.ciontekposservice.ICiontekPosService))) {
return ((com.ciontek.ciontekposservice.ICiontekPosService)iin);
}
return new com.ciontek.ciontekposservice.ICiontekPosService.Stub.Proxy(obj);
}
@Override public android.os.IBinder asBinder()
{
return this;
}
@Override public boolean onTransact(int code, android.os.Parcel data, android.os.Parcel reply, int flags) throws android.os.RemoteException
{
java.lang.String descriptor = DESCRIPTOR;
switch (code)
{
case INTERFACE_TRANSACTION:
{
reply.writeString(descriptor);
return true;
}
case TRANSACTION_installRomPackage:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
int _result = this.installRomPackage(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_getOSVersion:
{
data.enforceInterface(descriptor);
java.lang.String _result = this.getOSVersion();
reply.writeNoException();
reply.writeString(_result);
return true;
}
case TRANSACTION_getDeviceId:
{
data.enforceInterface(descriptor);
java.lang.String _result = this.getDeviceId();
reply.writeNoException();
reply.writeString(_result);
return true;
}
case TRANSACTION_Lib_LogSwitch:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_LogSwitch(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_GetRand:
{
data.enforceInterface(descriptor);
byte[] _arg0;
int _arg0_length = data.readInt();
if ((_arg0_length<0)) {
_arg0 = null;
}
else {
_arg0 = new byte[_arg0_length];
}
int _result = this.Lib_GetRand(_arg0);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg0);
return true;
}
case TRANSACTION_Lib_Update_32550:
{
data.enforceInterface(descriptor);
int _result = this.Lib_Update_32550();
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_GetVersion:
{
data.enforceInterface(descriptor);
byte[] _arg0;
int _arg0_length = data.readInt();
if ((_arg0_length<0)) {
_arg0 = null;
}
else {
_arg0 = new byte[_arg0_length];
}
int _result = this.Lib_GetVersion(_arg0);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg0);
return true;
}
case TRANSACTION_Lib_ReadSN:
{
data.enforceInterface(descriptor);
byte[] _arg0;
int _arg0_length = data.readInt();
if ((_arg0_length<0)) {
_arg0 = null;
}
else {
_arg0 = new byte[_arg0_length];
}
int _result = this.Lib_ReadSN(_arg0);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg0);
return true;
}
case TRANSACTION_Lib_WriteSN:
{
data.enforceInterface(descriptor);
byte[] _arg0;
_arg0 = data.createByteArray();
int _result = this.Lib_WriteSN(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_ReadChipID:
{
data.enforceInterface(descriptor);
byte[] _arg0;
int _arg0_length = data.readInt();
if ((_arg0_length<0)) {
_arg0 = null;
}
else {
_arg0 = new byte[_arg0_length];
}
int _arg1;
_arg1 = data.readInt();
int _result = this.Lib_ReadChipID(_arg0, _arg1);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg0);
return true;
}
case TRANSACTION_Lib_IccOpen:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
byte _arg1;
_arg1 = data.readByte();
byte[] _arg2;
int _arg2_length = data.readInt();
if ((_arg2_length<0)) {
_arg2 = null;
}
else {
_arg2 = new byte[_arg2_length];
}
int _result = this.Lib_IccOpen(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg2);
return true;
}
case TRANSACTION_Lib_IccClose:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
int _result = this.Lib_IccClose(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_IccCommand:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
byte[] _arg1;
_arg1 = data.createByteArray();
byte[] _arg2;
int _arg2_length = data.readInt();
if ((_arg2_length<0)) {
_arg2 = null;
}
else {
_arg2 = new byte[_arg2_length];
}
int _result = this.Lib_IccCommand(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg2);
return true;
}
case TRANSACTION_Lib_IccCheck:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
int _result = this.Lib_IccCheck(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_SC_ApduCmd:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
byte[] _arg1;
_arg1 = data.createByteArray();
int _arg2;
_arg2 = data.readInt();
byte[] _arg3;
int _arg3_length = data.readInt();
if ((_arg3_length<0)) {
_arg3 = null;
}
else {
_arg3 = new byte[_arg3_length];
}
byte[] _arg4;
int _arg4_length = data.readInt();
if ((_arg4_length<0)) {
_arg4 = null;
}
else {
_arg4 = new byte[_arg4_length];
}
int _result = this.SC_ApduCmd(_arg0, _arg1, _arg2, _arg3, _arg4);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg3);
reply.writeByteArray(_arg4);
return true;
}
case TRANSACTION_Lib_PrnInit:
{
data.enforceInterface(descriptor);
int _result = this.Lib_PrnInit();
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetSpace:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
byte _arg1;
_arg1 = data.readByte();
int _result = this.Lib_PrnSetSpace(_arg0, _arg1);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetFont:
{
data.enforceInterface(descriptor);
byte _arg0;
_arg0 = data.readByte();
byte _arg1;
_arg1 = data.readByte();
byte _arg2;
_arg2 = data.readByte();
int _result = this.Lib_PrnSetFont(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnGetFont:
{
data.enforceInterface(descriptor);
byte[] _arg0;
_arg0 = data.createByteArray();
byte[] _arg1;
_arg1 = data.createByteArray();
byte[] _arg2;
_arg2 = data.createByteArray();
int _result = this.Lib_PrnGetFont(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnStep:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnStep(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetVoltage:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetVoltage(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnIsCharge:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnIsCharge(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnStr:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
int _result = this.Lib_PrnStr(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnBmp:
{
data.enforceInterface(descriptor);
android.graphics.Bitmap _arg0;
if ((0!=data.readInt())) {
_arg0 = android.graphics.Bitmap.CREATOR.createFromParcel(data);
}
else {
_arg0 = null;
}
int _result = this.Lib_PrnBmp(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnBarcode:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
int _arg1;
_arg1 = data.readInt();
int _arg2;
_arg2 = data.readInt();
java.lang.String _arg3;
_arg3 = data.readString();
int _result = this.Lib_PrnBarcode(_arg0, _arg1, _arg2, _arg3);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrintCutQrCode:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
int _arg1;
_arg1 = data.readInt();
int _arg2;
_arg2 = data.readInt();
java.lang.String _arg3;
_arg3 = data.readString();
int _result = this.Lib_PrintCutQrCode(_arg0, _arg1, _arg2, _arg3);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrintCutQrCodeStr:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
java.lang.String _arg1;
_arg1 = data.readString();
int _arg2;
_arg2 = data.readInt();
int _arg3;
_arg3 = data.readInt();
int _arg4;
_arg4 = data.readInt();
java.lang.String _arg5;
_arg5 = data.readString();
int _result = this.Lib_PrintCutQrCodeStr(_arg0, _arg1, _arg2, _arg3, _arg4, _arg5);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnLogo:
{
data.enforceInterface(descriptor);
byte[] _arg0;
_arg0 = data.createByteArray();
int _result = this.Lib_PrnLogo(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_SetLinPixelDis:
{
data.enforceInterface(descriptor);
char _arg0;
_arg0 = (char)data.readInt();
int _result = this.Lib_SetLinPixelDis(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnStart:
{
data.enforceInterface(descriptor);
int _result = this.Lib_PrnStart();
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetLeftIndent:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetLeftIndent(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetAlign:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetAlign(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetCharSpace:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetCharSpace(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetLineSpace:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetLineSpace(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetLeftSpace:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetLeftSpace(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetGray:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetGray(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetSpeed:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetSpeed(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnCheckStatus:
{
data.enforceInterface(descriptor);
int _result = this.Lib_PrnCheckStatus();
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnFeedPaper:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnFeedPaper(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetMode:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetMode(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetUnderline:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetUnderline(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetReverse:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetReverse(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_Lib_PrnSetBold:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _result = this.Lib_PrnSetBold(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_enableAppInstallWhiteList:
{
data.enforceInterface(descriptor);
boolean _result = this.enableAppInstallWhiteList();
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_disableAppInstallWhiteList:
{
data.enforceInterface(descriptor);
boolean _result = this.disableAppInstallWhiteList();
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_addAppToInstallWhiteList:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
boolean _result = this.addAppToInstallWhiteList(_arg0);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_delAppFromInstallWhiteList:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
boolean _result = this.delAppFromInstallWhiteList(_arg0);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_getAppInstallWhiteList:
{
data.enforceInterface(descriptor);
java.util.List<java.lang.String> _result = this.getAppInstallWhiteList();
reply.writeNoException();
reply.writeStringList(_result);
return true;
}
case TRANSACTION_enableAppUninstallBlackList:
{
data.enforceInterface(descriptor);
boolean _result = this.enableAppUninstallBlackList();
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_disableAppUninstallBlackList:
{
data.enforceInterface(descriptor);
boolean _result = this.disableAppUninstallBlackList();
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_addAppToUninstallBlackList:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
boolean _result = this.addAppToUninstallBlackList(_arg0);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_delAppFromUninstallBlackList:
{
data.enforceInterface(descriptor);
java.lang.String _arg0;
_arg0 = data.readString();
boolean _result = this.delAppFromUninstallBlackList(_arg0);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_getAppUninstallBlackList:
{
data.enforceInterface(descriptor);
java.util.List<java.lang.String> _result = this.getAppUninstallBlackList();
reply.writeNoException();
reply.writeStringList(_result);
return true;
}
case TRANSACTION_fiscalOpen:
{
data.enforceInterface(descriptor);
int _arg0;
_arg0 = data.readInt();
int _arg1;
_arg1 = data.readInt();
int _arg2;
_arg2 = data.readInt();
char _arg3;
_arg3 = (char)data.readInt();
char _arg4;
_arg4 = (char)data.readInt();
int _result = this.fiscalOpen(_arg0, _arg1, _arg2, _arg3, _arg4);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_fiscalClose:
{
data.enforceInterface(descriptor);
int _result = this.fiscalClose();
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_fiscalWrite:
{
data.enforceInterface(descriptor);
byte[] _arg0;
_arg0 = data.createByteArray();
int _result = this.fiscalWrite(_arg0);
reply.writeNoException();
reply.writeInt(_result);
return true;
}
case TRANSACTION_fiscalRead:
{
data.enforceInterface(descriptor);
byte[] _arg0;
int _arg0_length = data.readInt();
if ((_arg0_length<0)) {
_arg0 = null;
}
else {
_arg0 = new byte[_arg0_length];
}
int _arg1;
_arg1 = data.readInt();
int _arg2;
_arg2 = data.readInt();
int _result = this.fiscalRead(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeInt(_result);
reply.writeByteArray(_arg0);
return true;
}
default:
{
return super.onTransact(code, data, reply, flags);
}
}
}
private static class Proxy implements com.ciontek.ciontekposservice.ICiontekPosService
{
private android.os.IBinder mRemote;
Proxy(android.os.IBinder remote)
{
mRemote = remote;
}
@Override public android.os.IBinder asBinder()
{
return mRemote;
}
public java.lang.String getInterfaceDescriptor()
{
return DESCRIPTOR;
}
@Override public int installRomPackage(java.lang.String romFilePath) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(romFilePath);
mRemote.transact(Stub.TRANSACTION_installRomPackage, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public java.lang.String getOSVersion() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
java.lang.String _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_getOSVersion, _data, _reply, 0);
_reply.readException();
_result = _reply.readString();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public java.lang.String getDeviceId() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
java.lang.String _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_getDeviceId, _data, _reply, 0);
_reply.readException();
_result = _reply.readString();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_LogSwitch(int LogSwitch) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(LogSwitch);
mRemote.transact(Stub.TRANSACTION_Lib_LogSwitch, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_GetRand(byte[] rnd) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((rnd==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(rnd.length);
}
mRemote.transact(Stub.TRANSACTION_Lib_GetRand, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(rnd);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_Update_32550() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_Lib_Update_32550, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_GetVersion(byte[] buf) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((buf==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(buf.length);
}
mRemote.transact(Stub.TRANSACTION_Lib_GetVersion, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(buf);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_ReadSN(byte[] SN) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((SN==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(SN.length);
}
mRemote.transact(Stub.TRANSACTION_Lib_ReadSN, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(SN);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_WriteSN(byte[] SN) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByteArray(SN);
mRemote.transact(Stub.TRANSACTION_Lib_WriteSN, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_ReadChipID(byte[] buf, int len) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((buf==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(buf.length);
}
_data.writeInt(len);
mRemote.transact(Stub.TRANSACTION_Lib_ReadChipID, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(buf);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_IccOpen(byte slot, byte vccMode, byte[] atr) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(slot);
_data.writeByte(vccMode);
if ((atr==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(atr.length);
}
mRemote.transact(Stub.TRANSACTION_Lib_IccOpen, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(atr);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_IccClose(byte slot) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(slot);
mRemote.transact(Stub.TRANSACTION_Lib_IccClose, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_IccCommand(byte slot, byte[] apduSend, byte[] apduResp) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(slot);
_data.writeByteArray(apduSend);
if ((apduResp==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(apduResp.length);
}
mRemote.transact(Stub.TRANSACTION_Lib_IccCommand, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(apduResp);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_IccCheck(byte slot) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(slot);
mRemote.transact(Stub.TRANSACTION_Lib_IccCheck, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int SC_ApduCmd(byte bslot, byte[] pbInApdu, int usInApduLen, byte[] pbOut, byte[] pbOutLen) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(bslot);
_data.writeByteArray(pbInApdu);
_data.writeInt(usInApduLen);
if ((pbOut==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(pbOut.length);
}
if ((pbOutLen==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(pbOutLen.length);
}
mRemote.transact(Stub.TRANSACTION_SC_ApduCmd, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(pbOut);
_reply.readByteArray(pbOutLen);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnInit() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_Lib_PrnInit, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetSpace(byte x, byte y) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(x);
_data.writeByte(y);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetSpace, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetFont(byte AsciiFontHeight, byte ExtendFontHeight, byte Zoom) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByte(AsciiFontHeight);
_data.writeByte(ExtendFontHeight);
_data.writeByte(Zoom);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetFont, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnGetFont(byte[] AsciiFontHeight, byte[] ExtendFontHeight, byte[] Zoom) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByteArray(AsciiFontHeight);
_data.writeByteArray(ExtendFontHeight);
_data.writeByteArray(Zoom);
mRemote.transact(Stub.TRANSACTION_Lib_PrnGetFont, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnStep(int pixel) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(pixel);
mRemote.transact(Stub.TRANSACTION_Lib_PrnStep, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetVoltage(int voltage) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(voltage);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetVoltage, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnIsCharge(int ischarge) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(ischarge);
mRemote.transact(Stub.TRANSACTION_Lib_PrnIsCharge, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnStr(java.lang.String str) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(str);
mRemote.transact(Stub.TRANSACTION_Lib_PrnStr, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnBmp(android.graphics.Bitmap bitmap) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((bitmap!=null)) {
_data.writeInt(1);
bitmap.writeToParcel(_data, 0);
}
else {
_data.writeInt(0);
}
mRemote.transact(Stub.TRANSACTION_Lib_PrnBmp, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnBarcode(java.lang.String contents, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(contents);
_data.writeInt(desiredWidth);
_data.writeInt(desiredHeight);
_data.writeString(barcodeType);
mRemote.transact(Stub.TRANSACTION_Lib_PrnBarcode, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrintCutQrCode(java.lang.String contents, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(contents);
_data.writeInt(desiredWidth);
_data.writeInt(desiredHeight);
_data.writeString(barcodeType);
mRemote.transact(Stub.TRANSACTION_Lib_PrintCutQrCode, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrintCutQrCodeStr(java.lang.String qrContent, java.lang.String printTxt, int distance, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(qrContent);
_data.writeString(printTxt);
_data.writeInt(distance);
_data.writeInt(desiredWidth);
_data.writeInt(desiredHeight);
_data.writeString(barcodeType);
mRemote.transact(Stub.TRANSACTION_Lib_PrintCutQrCodeStr, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnLogo(byte[] logo) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByteArray(logo);
mRemote.transact(Stub.TRANSACTION_Lib_PrnLogo, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_SetLinPixelDis(char LinDistance) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(((int)LinDistance));
mRemote.transact(Stub.TRANSACTION_Lib_SetLinPixelDis, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnStart() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_Lib_PrnStart, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetLeftIndent(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetLeftIndent, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetAlign(int X) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(X);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetAlign, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetCharSpace(int X) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(X);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetCharSpace, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetLineSpace(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetLineSpace, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetLeftSpace(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetLeftSpace, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetGray(int nLevel) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(nLevel);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetGray, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetSpeed(int iSpeed) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(iSpeed);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetSpeed, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnCheckStatus() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_Lib_PrnCheckStatus, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnFeedPaper(int step) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(step);
mRemote.transact(Stub.TRANSACTION_Lib_PrnFeedPaper, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetMode(int mode) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(mode);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetMode, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetUnderline(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetUnderline, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetReverse(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetReverse, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int Lib_PrnSetBold(int x) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(x);
mRemote.transact(Stub.TRANSACTION_Lib_PrnSetBold, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
/*---------------------------  APP White List  -----------------------------------------------*///5.0.1

@Override public boolean enableAppInstallWhiteList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_enableAppInstallWhiteList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.0.2

@Override public boolean disableAppInstallWhiteList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_disableAppInstallWhiteList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.0.3

@Override public boolean addAppToInstallWhiteList(java.lang.String pkgName) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(pkgName);
mRemote.transact(Stub.TRANSACTION_addAppToInstallWhiteList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.0.4

@Override public boolean delAppFromInstallWhiteList(java.lang.String pkgName) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(pkgName);
mRemote.transact(Stub.TRANSACTION_delAppFromInstallWhiteList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.0.5

@Override public java.util.List<java.lang.String> getAppInstallWhiteList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
java.util.List<java.lang.String> _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_getAppInstallWhiteList, _data, _reply, 0);
_reply.readException();
_result = _reply.createStringArrayList();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.1.1

@Override public boolean enableAppUninstallBlackList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_enableAppUninstallBlackList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.1.2

@Override public boolean disableAppUninstallBlackList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_disableAppUninstallBlackList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.1.3

@Override public boolean addAppToUninstallBlackList(java.lang.String pkgName) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(pkgName);
mRemote.transact(Stub.TRANSACTION_addAppToUninstallBlackList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.1.4

@Override public boolean delAppFromUninstallBlackList(java.lang.String pkgName) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(pkgName);
mRemote.transact(Stub.TRANSACTION_delAppFromUninstallBlackList, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
//5.1.5

@Override public java.util.List<java.lang.String> getAppUninstallBlackList() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
java.util.List<java.lang.String> _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_getAppUninstallBlackList, _data, _reply, 0);
_reply.readException();
_result = _reply.createStringArrayList();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int fiscalOpen(int baudrate, int size, int stop, char parity, char cflow) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeInt(baudrate);
_data.writeInt(size);
_data.writeInt(stop);
_data.writeInt(((int)parity));
_data.writeInt(((int)cflow));
mRemote.transact(Stub.TRANSACTION_fiscalOpen, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int fiscalClose() throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
mRemote.transact(Stub.TRANSACTION_fiscalClose, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int fiscalWrite(byte[] data) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeByteArray(data);
mRemote.transact(Stub.TRANSACTION_fiscalWrite, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
@Override public int fiscalRead(byte[] buffer, int bufLen, int timeout) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
int _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
if ((buffer==null)) {
_data.writeInt(-1);
}
else {
_data.writeInt(buffer.length);
}
_data.writeInt(bufLen);
_data.writeInt(timeout);
mRemote.transact(Stub.TRANSACTION_fiscalRead, _data, _reply, 0);
_reply.readException();
_result = _reply.readInt();
_reply.readByteArray(buffer);
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
}
static final int TRANSACTION_installRomPackage = (android.os.IBinder.FIRST_CALL_TRANSACTION + 0);
static final int TRANSACTION_getOSVersion = (android.os.IBinder.FIRST_CALL_TRANSACTION + 1);
static final int TRANSACTION_getDeviceId = (android.os.IBinder.FIRST_CALL_TRANSACTION + 2);
static final int TRANSACTION_Lib_LogSwitch = (android.os.IBinder.FIRST_CALL_TRANSACTION + 3);
static final int TRANSACTION_Lib_GetRand = (android.os.IBinder.FIRST_CALL_TRANSACTION + 4);
static final int TRANSACTION_Lib_Update_32550 = (android.os.IBinder.FIRST_CALL_TRANSACTION + 5);
static final int TRANSACTION_Lib_GetVersion = (android.os.IBinder.FIRST_CALL_TRANSACTION + 6);
static final int TRANSACTION_Lib_ReadSN = (android.os.IBinder.FIRST_CALL_TRANSACTION + 7);
static final int TRANSACTION_Lib_WriteSN = (android.os.IBinder.FIRST_CALL_TRANSACTION + 8);
static final int TRANSACTION_Lib_ReadChipID = (android.os.IBinder.FIRST_CALL_TRANSACTION + 9);
static final int TRANSACTION_Lib_IccOpen = (android.os.IBinder.FIRST_CALL_TRANSACTION + 10);
static final int TRANSACTION_Lib_IccClose = (android.os.IBinder.FIRST_CALL_TRANSACTION + 11);
static final int TRANSACTION_Lib_IccCommand = (android.os.IBinder.FIRST_CALL_TRANSACTION + 12);
static final int TRANSACTION_Lib_IccCheck = (android.os.IBinder.FIRST_CALL_TRANSACTION + 13);
static final int TRANSACTION_SC_ApduCmd = (android.os.IBinder.FIRST_CALL_TRANSACTION + 14);
static final int TRANSACTION_Lib_PrnInit = (android.os.IBinder.FIRST_CALL_TRANSACTION + 15);
static final int TRANSACTION_Lib_PrnSetSpace = (android.os.IBinder.FIRST_CALL_TRANSACTION + 16);
static final int TRANSACTION_Lib_PrnSetFont = (android.os.IBinder.FIRST_CALL_TRANSACTION + 17);
static final int TRANSACTION_Lib_PrnGetFont = (android.os.IBinder.FIRST_CALL_TRANSACTION + 18);
static final int TRANSACTION_Lib_PrnStep = (android.os.IBinder.FIRST_CALL_TRANSACTION + 19);
static final int TRANSACTION_Lib_PrnSetVoltage = (android.os.IBinder.FIRST_CALL_TRANSACTION + 20);
static final int TRANSACTION_Lib_PrnIsCharge = (android.os.IBinder.FIRST_CALL_TRANSACTION + 21);
static final int TRANSACTION_Lib_PrnStr = (android.os.IBinder.FIRST_CALL_TRANSACTION + 22);
static final int TRANSACTION_Lib_PrnBmp = (android.os.IBinder.FIRST_CALL_TRANSACTION + 23);
static final int TRANSACTION_Lib_PrnBarcode = (android.os.IBinder.FIRST_CALL_TRANSACTION + 24);
static final int TRANSACTION_Lib_PrintCutQrCode = (android.os.IBinder.FIRST_CALL_TRANSACTION + 25);
static final int TRANSACTION_Lib_PrintCutQrCodeStr = (android.os.IBinder.FIRST_CALL_TRANSACTION + 26);
static final int TRANSACTION_Lib_PrnLogo = (android.os.IBinder.FIRST_CALL_TRANSACTION + 27);
static final int TRANSACTION_Lib_SetLinPixelDis = (android.os.IBinder.FIRST_CALL_TRANSACTION + 28);
static final int TRANSACTION_Lib_PrnStart = (android.os.IBinder.FIRST_CALL_TRANSACTION + 29);
static final int TRANSACTION_Lib_PrnSetLeftIndent = (android.os.IBinder.FIRST_CALL_TRANSACTION + 30);
static final int TRANSACTION_Lib_PrnSetAlign = (android.os.IBinder.FIRST_CALL_TRANSACTION + 31);
static final int TRANSACTION_Lib_PrnSetCharSpace = (android.os.IBinder.FIRST_CALL_TRANSACTION + 32);
static final int TRANSACTION_Lib_PrnSetLineSpace = (android.os.IBinder.FIRST_CALL_TRANSACTION + 33);
static final int TRANSACTION_Lib_PrnSetLeftSpace = (android.os.IBinder.FIRST_CALL_TRANSACTION + 34);
static final int TRANSACTION_Lib_PrnSetGray = (android.os.IBinder.FIRST_CALL_TRANSACTION + 35);
static final int TRANSACTION_Lib_PrnSetSpeed = (android.os.IBinder.FIRST_CALL_TRANSACTION + 36);
static final int TRANSACTION_Lib_PrnCheckStatus = (android.os.IBinder.FIRST_CALL_TRANSACTION + 37);
static final int TRANSACTION_Lib_PrnFeedPaper = (android.os.IBinder.FIRST_CALL_TRANSACTION + 38);
static final int TRANSACTION_Lib_PrnSetMode = (android.os.IBinder.FIRST_CALL_TRANSACTION + 39);
static final int TRANSACTION_Lib_PrnSetUnderline = (android.os.IBinder.FIRST_CALL_TRANSACTION + 40);
static final int TRANSACTION_Lib_PrnSetReverse = (android.os.IBinder.FIRST_CALL_TRANSACTION + 41);
static final int TRANSACTION_Lib_PrnSetBold = (android.os.IBinder.FIRST_CALL_TRANSACTION + 42);
static final int TRANSACTION_enableAppInstallWhiteList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 43);
static final int TRANSACTION_disableAppInstallWhiteList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 44);
static final int TRANSACTION_addAppToInstallWhiteList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 45);
static final int TRANSACTION_delAppFromInstallWhiteList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 46);
static final int TRANSACTION_getAppInstallWhiteList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 47);
static final int TRANSACTION_enableAppUninstallBlackList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 48);
static final int TRANSACTION_disableAppUninstallBlackList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 49);
static final int TRANSACTION_addAppToUninstallBlackList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 50);
static final int TRANSACTION_delAppFromUninstallBlackList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 51);
static final int TRANSACTION_getAppUninstallBlackList = (android.os.IBinder.FIRST_CALL_TRANSACTION + 52);
static final int TRANSACTION_fiscalOpen = (android.os.IBinder.FIRST_CALL_TRANSACTION + 53);
static final int TRANSACTION_fiscalClose = (android.os.IBinder.FIRST_CALL_TRANSACTION + 54);
static final int TRANSACTION_fiscalWrite = (android.os.IBinder.FIRST_CALL_TRANSACTION + 55);
static final int TRANSACTION_fiscalRead = (android.os.IBinder.FIRST_CALL_TRANSACTION + 56);
}
public int installRomPackage(java.lang.String romFilePath) throws android.os.RemoteException;
public java.lang.String getOSVersion() throws android.os.RemoteException;
public java.lang.String getDeviceId() throws android.os.RemoteException;
public int Lib_LogSwitch(int LogSwitch) throws android.os.RemoteException;
public int Lib_GetRand(byte[] rnd) throws android.os.RemoteException;
public int Lib_Update_32550() throws android.os.RemoteException;
public int Lib_GetVersion(byte[] buf) throws android.os.RemoteException;
public int Lib_ReadSN(byte[] SN) throws android.os.RemoteException;
public int Lib_WriteSN(byte[] SN) throws android.os.RemoteException;
public int Lib_ReadChipID(byte[] buf, int len) throws android.os.RemoteException;
public int Lib_IccOpen(byte slot, byte vccMode, byte[] atr) throws android.os.RemoteException;
public int Lib_IccClose(byte slot) throws android.os.RemoteException;
public int Lib_IccCommand(byte slot, byte[] apduSend, byte[] apduResp) throws android.os.RemoteException;
public int Lib_IccCheck(byte slot) throws android.os.RemoteException;
public int SC_ApduCmd(byte bslot, byte[] pbInApdu, int usInApduLen, byte[] pbOut, byte[] pbOutLen) throws android.os.RemoteException;
public int Lib_PrnInit() throws android.os.RemoteException;
public int Lib_PrnSetSpace(byte x, byte y) throws android.os.RemoteException;
public int Lib_PrnSetFont(byte AsciiFontHeight, byte ExtendFontHeight, byte Zoom) throws android.os.RemoteException;
public int Lib_PrnGetFont(byte[] AsciiFontHeight, byte[] ExtendFontHeight, byte[] Zoom) throws android.os.RemoteException;
public int Lib_PrnStep(int pixel) throws android.os.RemoteException;
public int Lib_PrnSetVoltage(int voltage) throws android.os.RemoteException;
public int Lib_PrnIsCharge(int ischarge) throws android.os.RemoteException;
public int Lib_PrnStr(java.lang.String str) throws android.os.RemoteException;
public int Lib_PrnBmp(android.graphics.Bitmap bitmap) throws android.os.RemoteException;
public int Lib_PrnBarcode(java.lang.String contents, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException;
public int Lib_PrintCutQrCode(java.lang.String contents, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException;
public int Lib_PrintCutQrCodeStr(java.lang.String qrContent, java.lang.String printTxt, int distance, int desiredWidth, int desiredHeight, java.lang.String barcodeType) throws android.os.RemoteException;
public int Lib_PrnLogo(byte[] logo) throws android.os.RemoteException;
public int Lib_SetLinPixelDis(char LinDistance) throws android.os.RemoteException;
public int Lib_PrnStart() throws android.os.RemoteException;
public int Lib_PrnSetLeftIndent(int x) throws android.os.RemoteException;
public int Lib_PrnSetAlign(int X) throws android.os.RemoteException;
public int Lib_PrnSetCharSpace(int X) throws android.os.RemoteException;
public int Lib_PrnSetLineSpace(int x) throws android.os.RemoteException;
public int Lib_PrnSetLeftSpace(int x) throws android.os.RemoteException;
public int Lib_PrnSetGray(int nLevel) throws android.os.RemoteException;
public int Lib_PrnSetSpeed(int iSpeed) throws android.os.RemoteException;
public int Lib_PrnCheckStatus() throws android.os.RemoteException;
public int Lib_PrnFeedPaper(int step) throws android.os.RemoteException;
public int Lib_PrnSetMode(int mode) throws android.os.RemoteException;
public int Lib_PrnSetUnderline(int x) throws android.os.RemoteException;
public int Lib_PrnSetReverse(int x) throws android.os.RemoteException;
public int Lib_PrnSetBold(int x) throws android.os.RemoteException;
/*---------------------------  APP White List  -----------------------------------------------*///5.0.1

public boolean enableAppInstallWhiteList() throws android.os.RemoteException;
//5.0.2

public boolean disableAppInstallWhiteList() throws android.os.RemoteException;
//5.0.3

public boolean addAppToInstallWhiteList(java.lang.String pkgName) throws android.os.RemoteException;
//5.0.4

public boolean delAppFromInstallWhiteList(java.lang.String pkgName) throws android.os.RemoteException;
//5.0.5

public java.util.List<java.lang.String> getAppInstallWhiteList() throws android.os.RemoteException;
//5.1.1

public boolean enableAppUninstallBlackList() throws android.os.RemoteException;
//5.1.2

public boolean disableAppUninstallBlackList() throws android.os.RemoteException;
//5.1.3

public boolean addAppToUninstallBlackList(java.lang.String pkgName) throws android.os.RemoteException;
//5.1.4

public boolean delAppFromUninstallBlackList(java.lang.String pkgName) throws android.os.RemoteException;
//5.1.5

public java.util.List<java.lang.String> getAppUninstallBlackList() throws android.os.RemoteException;
public int fiscalOpen(int baudrate, int size, int stop, char parity, char cflow) throws android.os.RemoteException;
public int fiscalClose() throws android.os.RemoteException;
public int fiscalWrite(byte[] data) throws android.os.RemoteException;
public int fiscalRead(byte[] buffer, int bufLen, int timeout) throws android.os.RemoteException;
}
