<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PKEx.aspx.cs" Inherits="PKEx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pirate Kings - nhothuy48cb@gmail.com</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body class="blurBg-false" style="background-color: #EBEBEB">
    <link rel="stylesheet" href="formoid_files/formoid1/formoid-metro-cyan.css" type="text/css" />
    <script type="text/javascript" src="formoid_files/formoid1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Result").click(function () {
                var accountName = $("#accountName").val();
                if (accountName == null || accountName == "") return;
                var isAttackRandom = $('#chkAttackRandom').is(":checked");
                var isStealAuto = $('#chkStealAuto').is(":checked");
                $("#imgIsLand").html("<img src='imgs/ajax-loader.gif'/>");
                $("#stealInfo").html("");
                $.ajax({
                    type: "POST",
                    url: "PKWS.asmx/PlayPK",
                    data: "{'accountName':'" + accountName + "','isAttackRandom':'" + isAttackRandom + "','isStealAuto':' " + isStealAuto + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {                        
                        var obj = jQuery.parseJSON(msg.d);
                        var wheelResult = obj.wheelResult;
                        $("#playerInfo").html(obj.playerInfo);
                        $("#cashKingInfo").html(obj.cashKingInfo);
                        $("#imgIsLand").html(obj.imgIsLand);
                        if (wheelResult == "6" && !isStealAuto) {
                            $("#stealInfo").html(obj.stealInfo);
                            alert("Steal...!!!");
                        }
                        if (!isAttackRandom && wheelResult == "7") alert("Attack...!!!");
                    }
                });
            });
            $("#ResultOne").click(function () {
                var accountName = $("#accountName").val();
                if (accountName == null || accountName == "") return;
                var isAttackRandom = $('#chkAttackRandom').is(":checked");
                var isStealAuto = $('#chkStealAuto').is(":checked");
                $("#imgIsLand").html("<img src='imgs/ajax-loader.gif'/>");
                $("#stealInfo").html("");
                $.ajax({
                    type: "POST",
                    url: "PKWS.asmx/PlayPKOne",
                    data: "{'accountName':'" + accountName + "','isAttackRandom':'" + isAttackRandom + "','isStealAuto':' " + isStealAuto + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var obj = jQuery.parseJSON(msg.d);
                        var wheelResult = obj.wheelResult;
                        $("#playerInfo").html(obj.playerInfo);
                        $("#cashKingInfo").html(obj.cashKingInfo);
                        $("#imgIsLand").html(obj.imgIsLand);
                        if (wheelResult == "6" && !isStealAuto) {
                            $("#stealInfo").html(obj.stealInfo);
                            alert("Steal...!!!");
                        }
                        if (!isAttackRandom && wheelResult == "7") alert("Attack...!!!");
                    }
                });
            });
        });
    </script>
    <form onsubmit="return false;" id="form1" class="formoid-metro-cyan" style="background-color: #1A2223; font-size: 14px; font-family: 'Open Sans','Helvetica Neue','Helvetica',Arial,Verdana,sans-serif; color: #666666; max-width: 480px; min-width: 100px" runat="server">
        <div class="title">
            <h2>Pirate Kings</h2>
        </div>
        <div class="element-textbox" title="Account">
            <label class="title">Account</label>
            <input type="text" id="accountName" maxlength="100"/>
        </div>
        <div class="element-checkbox">
            <label class="title"></label>
            <div class="column column1">
                <label>
                    <input id="chkAttackRandom" type="checkbox" value="Attack Random" checked="checked"/><span>Attack Random</span>
                </label>
                <label>
                    <input id="chkStealAuto" type="checkbox" value="Steal Auto" checked="checked"/><span>Steal Auto</span>
                </label>
            </div>
            <label class="title" id="playerInfo">Rank:0 Shields:0 Spins:0 Cash:0 NextSpin: 0</label>
            <label class="title" id="cashKingInfo">Name: Rank:0 Cash:0</label>
            <label class="title" id="stealInfo"></label>
            <div align="center" id="imgIsLand">
            </div>
            <span class="clearfix"></span>
        </div>
        <div class="submit">
            <input type="submit" value="Spin" id="ResultOne"/>
            &nbsp;
            <input type="submit" value="Spin All" id="Result"/>
        </div>                  
    </form>
</body>
</html>
