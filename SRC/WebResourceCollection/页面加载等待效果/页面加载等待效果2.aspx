<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ҳ����صȴ�Ч��2.aspx.cs" Inherits="WebResourceCollection.ҳ����صȴ�Ч��2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�����ύ�ȴ�</title>
</head>
<body>
    <script language="javascript" type="text/javascript">
    function showSending() {
        sending.style.visibility="visible";
    }
    </script>
    <form method="post" action="">
        <div id="sending" style="position: absolute; z-index: 10; width: 400; visibility: hidden">
            <table width="400" height="80" border="0" cellspacing="2" cellpadding="0" bgcolor="#8FA8E9">
                <tr>
                    <td bgcolor="#eeeeee" align="center">�������ڷ���, ���Ժ�...</td>
                </tr>
            </table>
        </div>
        <table width="95%" border="1" cellspacing="0" cellpadding="1" bordercolorlight="#8FA8E9" bordercolordark="#FFFFFF">
            <tr align="center">
                <td height="30" class="bg3" colspan="2">
                    <input type='submit' name='ACTION' value='����' onclick="showSending()">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
