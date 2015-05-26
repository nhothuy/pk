<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PK.aspx.cs" Inherits="PK" %>
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
                var accountName = $("#accountName option:selected").val();
                var isAttackRandom = $('#chkAttackRandom').is(":checked");
                var isStealAuto = $('#chkStealAuto').is(":checked");
                $("#imgIsLand").html("<img src='imgs/ajax-loader.gif'/>");
                $("#stealInfo").html("");
                $.ajax({
                    type: "POST",
                    url: "PK.aspx/PlayPK",
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
                var accountName = $("#accountName option:selected").val();
                var isAttackRandom = $('#chkAttackRandom').is(":checked");
                var isStealAuto = $('#chkStealAuto').is(":checked");
                $("#imgIsLand").html("<img src='imgs/ajax-loader.gif'/>");
                $("#stealInfo").html("");
                $.ajax({
                    type: "POST",
                    url: "PK.aspx/PlayPKOne",
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
        <div class="element-select" title="Account">
            <label class="title">Account</label>
            <div class="medium">
                <span>
                    <select name="select" id="accountName">
                        <option value="5541b1ac94c0430f980e0cc9">0969023566</option>
                        <option value="5543d17a94c0420fd8b9d13f">0912901720</option>
                        <option value="5541320594c0430ff49f3672">0974260220</option>
                        <option value="55412a8394c04106c03394a7">0944220487</option>
                        <option value="5532757194c0430b8c2c9973">nhothuy48cb</option>
                        <option value="553d2d6994c03f0ed4dd44d8">lethiminhht</option>
                    </select>
                    <i></i>
                </span>
            </div>
        </div>
        <div class="element-checkbox">
            <label class="title"></label>
            <div class="column column1">
                <label>
                    <input id="chkAttackRandom" type="checkbox" value="Attack Random" checked="checked"/><span>Attack Random</span>
                </label>
                <label>
                    <input id="chkStealAuto" type="checkbox" value="Steal Auto"/><span>Steal Auto</span>
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
