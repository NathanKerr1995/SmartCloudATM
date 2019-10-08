<%@ Page Title="" Language="C#" MasterPageFile="~/MainStyle.Master" AutoEventWireup="true" CodeBehind="MainScreen.aspx.cs" Inherits="ATM.MainScreen" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            
            if (document.getElementById('<%=hfReceipt5.ClientID%>').value == 0) {
                document.getElementById('<%=btnReceipt5.ClientID%>').disabled = true;
            };

            if (document.getElementById('<%=hfReceipt10.ClientID%>').value == 0) {
                document.getElementById('<%=btnReceipt10.ClientID%>').disabled = true;
            };

            if (document.getElementById('<%=hfReceipt20.ClientID%>').value == 0) {
                document.getElementById('<%=btnReceipt20.ClientID%>').disabled = true;
            };

            if (document.getElementById('<%=hfReceipt50.ClientID%>').value == 0) {
                document.getElementById('<%=btnReceipt50.ClientID%>').disabled = true;
            };

            if (document.getElementById('<%=hfReceipt100.ClientID%>').value == 0) {
                document.getElementById('<%=btnReceipt100.ClientID%>').disabled = true;
            };
});
    </script>
    <script type="text/javascript">
    function openModal() {
        $('#mdlMessage').modal('show');
        }

        function openModalTimeout() {
            $('#mdlTimeout').modal('show');
            var timout = 10000; // Timeout in 10 seconds.
            timeoutTimer = setTimeout("indexTimeout()", timout);
        }

        // Logout the user.
        function indexTimeout() {
            window.location = 'Index.aspx';;
        }
</script>

    <script>
         function showCheckBalancePanel()
         {
             document.getElementById('<%=pnlCheckBalance.ClientID%>').style.display = "";
             document.getElementById('<%=pnlWithdraw.ClientID%>').style.display = "none";
        }  

        function showWithdrawPanel()
         {
             document.getElementById('<%=pnlWithdraw.ClientID%>').style.display = "";
             document.getElementById('<%=pnlCheckBalance.ClientID%>').style.display = "none";
         }  
    </script>

    <script>
         function showModalForReceipt(Amount)
         {
             document.getElementById('<%=txtOtherAmount.ClientID%>').value = null;
             //check if withdraw is more than users balance
             if (Amount > parseInt(document.getElementById('<%=lblBalance.ClientID%>').innerHTML.slice(10), 10)) {
                 $('#mdlOtherAmount').modal('hide');
                $('#mdlOverBalance').modal('show');

                 document.getElementById('<%=lblModalOverBalanceTitle.ClientID%>').innerHTML = 'Over Account Balance'
                 document.getElementById('<%=lblModalOverBalanceMessage.ClientID%>').innerHTML = 'This withdrawal will put you into your overdraft. Are you sure you want to withdraw?'
                 document.getElementById('<%=hfOtherAmount.ClientID%>').value = Amount;
             }
             else {
                 $('#mdlOtherAmount').modal('hide');
                 $('#mdlOverBalance').modal('hide');
                $('#mdlReceipt').modal('show');

                 document.getElementById('<%=lblModalReceiptTitle.ClientID%>').innerHTML = 'Receipt';
                 document.getElementById('<%=lblModalReceiptMessage.ClientID%>').innerHTML = 'Would you like a receipt of your withdrawal emailed to you?';
                 document.getElementById('<%=hfOtherAmount.ClientID%>').value = Amount;
             }
        }

        function showModalForReceiptOverBalance()
        {
            $('#mdlOtherAmount').modal('hide');
            $('#mdlOverBalance').modal('hide');
            $('#mdlReceipt').modal('show');

            document.getElementById('<%=lblModalReceiptTitle.ClientID%>').innerHTML = 'Receipt';
            document.getElementById('<%=lblModalReceiptMessage.ClientID%>').innerHTML = 'Would you like a receipt of your withdrawal emailed to you?';
            document.getElementById('<%=hfOtherAmount.ClientID%>').value = document.getElementById('<%=hfOtherAmount.ClientID%>').value;
        }

        function showModalForOther()
         {
             $('#mdlOtherAmount').modal('show');
            document.getElementById('<%=hfOtherAmount.ClientID%>').value = null;
        }  
    </script>

    <script type="text/javascript"> 
        function numPadAddNum(num) {
            if (document.getElementById('<%= txtOtherAmount.ClientID %>').value.length <= 3) {
                var txt = document.getElementById('<%= txtOtherAmount.ClientID %>').value;
                txt = txt + num;
                document.getElementById('<%= txtOtherAmount.ClientID %>').value = txt;
                $("#ContentPlaceHolder1_txtOtherAmount").focusTextToEnd();
            }
            else {
                $("#ContentPlaceHolder1_txtOtherAmount").focusTextToEnd();
            }
        }

        function numPadClear() {
            document.getElementById('<%= txtOtherAmount.ClientID %>').value = null;
            $("#ContentPlaceHolder1_txtOtherAmount").focusTextToEnd();
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            (function ($) {
                $.fn.inputFilter = function (inputFilter) {
                    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                        if (inputFilter(this.value)) {
                            this.oldValue = this.value;
                            this.oldSelectionStart = this.selectionStart;
                            this.oldSelectionEnd = this.selectionEnd;
                        } else if (this.hasOwnProperty("oldValue")) {
                            this.value = this.oldValue;
                            this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                        }
                    });
                };
            }(jQuery));

            // Install input filters.
            $("#ContentPlaceHolder1_txtOtherAmount").inputFilter(function (value) {
                return /^\d*$/.test(value);
            });
        });

        (function ($) {
            $.fn.focusTextToEnd = function () {
                this.focus();
                var $thisVal = this.val();
                this.val('').val($thisVal);
                return this;
            }
        }(jQuery));
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfOtherAmount" runat="server" />
    <asp:HiddenField ID="hfReceipt5" runat="server" />
    <asp:HiddenField ID="hfReceipt10" runat="server" />
    <asp:HiddenField ID="hfReceipt20" runat="server" />
    <asp:HiddenField ID="hfReceipt50" runat="server" />
    <asp:HiddenField ID="hfReceipt100" runat="server" />
        <div class="form-row" style="width:100%; height:20%;">
            <div class="form-group col-md-2">
                </div>
            <div class="form-group col-md-3 text-center" >
                <input type="button" value="Check Balance" class="btn btn-primary btn-lg" style="width: 100%; height: 50%;" onclick="showCheckBalancePanel()">
                </div>
            <div class="form-group col-md-3 text-center" >
                <input type="button" value="Withdraw" class="btn btn-primary btn-lg" style="width: 100%; height: 50%;" onclick="showWithdrawPanel()">
                </div>
            <div class="form-group col-md-2 text-center" >
            <asp:Button ID="btnCancel" runat="server" class="btn btn-primary btn-lg" Style="width: 100%; height: 50%;" Text="Cancel" OnClick="btnCancel_Click"  />
                </div>
            <div class="form-group col-md-2">
                <asp:LinkButton ID="lnbtSpeak" CssClass="btn-lg float-right" runat="server" OnClick="lnbtSpeak_Click" Font-Size="XX-Large"><i class='fa fa-volume-up'></i></asp:LinkButton>
                </div>
            </div>

        <div class="form-row" style="width:100%; height:70%;">
            
            <div class="form-group col-md-2">
                </div>
            <div class="form-group col-md-8 main-screen-box" >
                <asp:Panel ID="pnlCheckBalance" runat="server" CssClass="row align-items-center justify-content-center" style="width:100%; height:100%; display:none;">
                    <h2 class="text-center row align-items-center justify-content-center">
                        <asp:Label ID="lblBalance" runat="server" CssClass="text-white font-weight-bold" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Button ID="btnEmailBalance" runat="server" class="btn btn-primary btn-lg" Style="width: 100%; height: 100%;" Text="Email Balance" OnClick="btnEmailBalance_Click" />
                        </h2>
                    </asp:Panel>
                <asp:Panel ID="pnlWithdraw" runat="server" CssClass="row align-items-center justify-content-center" style="width:100%; height:100%; display:none;">
                    <div class="form-group col-md-5" style="width:100%; height:100%;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnReceipt5" name="btnReceipt5" runat="server" value="£5" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForReceipt(5)">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnReceipt10" name="btnReceipt10" runat="server" value="£10" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForReceipt(10)">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnReceipt20" name="btnReceipt20" runat="server" value="£20" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForReceipt(20)">
                </div>
                    <div class="form-group col-md-2">
                </div>
                    <div class="form-group col-md-5" style="width:100%; height:100%;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnReceipt50" name="btnReceipt50" runat="server" value="£50" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForReceipt(50)">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnReceipt100" name="btnReceipt100" runat="server" value="£100" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForReceipt(100)">
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" value="Other" class="btn btn-primary btn-lg" style="width: 100%; height: 15%;" onclick="showModalForOther()">
                </div>
                    </asp:Panel>
                </div>
        <div class="form-group col-md-2">
                </div>
        </div>

    <div class="col-sm-12">
                <div class="modal fade" id="mdlMessage" data-backdrop="static" data-keyboard="false" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <p><asp:Label ID="lblModalMessage" runat="server" Text=""></asp:Label></p>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-primary" data-dismiss="modal">Okay</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <div class="col-sm-12">
                <div class="modal fade" id="mdlTimeout" data-backdrop="static" data-keyboard="false" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTimeoutTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <p><asp:Label ID="lblModalTimeoutMessage" runat="server" Text=""></asp:Label></p>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <div class="col-xs-12">
                <div class="modal fade" id="mdlReceipt" data-backdrop="static" data-keyboard="false" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalReceiptTitle" runat="server" Text=""></asp:Label></h4>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <p><asp:Label ID="lblModalReceiptMessage" runat="server" Text=""></asp:Label></p>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnYesRecipt" runat="server" CssClass="btn btn-success btn-lg" Text="Yes" OnClick="btnYesRecipt_Click" />
                                <asp:Button ID="btnNoRecipt" runat="server" CssClass="btn btn-danger btn-lg" Text="No" OnClick="btnNoRecipt_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <div class="col-xs-12">
                <div class="modal fade" id="mdlOverBalance" data-backdrop="static" data-keyboard="false" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalOverBalanceTitle" runat="server" Text=""></asp:Label></h4>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <p><asp:Label ID="lblModalOverBalanceMessage" runat="server" Text=""></asp:Label></p>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input type="button" value="Yes" class="btn btn-success btn-lg" onclick="showModalForReceiptOverBalance()">
                                <button class="btn btn-danger btn-lg" data-dismiss="modal">No</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <div class="col-xl-12">
                <div class="modal fade" id="mdlOtherAmount" data-backdrop="static" data-keyboard="false" tabindex="-1">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblOtherAmountTitle" runat="server" Text="Please enter amount"></asp:Label></h4>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <asp:Panel ID="pnlNumberButtons" runat="server" style="width:100%; height:90%;">
        <div class="form-row" style="width:100%; height:100%;">
            <div class="form-group col-md-2">
                </div>
            <div class="form-group col-md-8 text-center login-textbox-outer" >
                    <h2>
                        <asp:Label ID="lblResponse" runat="server" CssClass="text-black font-weight-bold" Text="Enter Amount"></asp:Label>
                        </h2>
            <asp:TextBox ID="txtOtherAmount" runat="server" CssClass="form-control" Width="90%" Font-Size="XX-Large"></asp:TextBox>
                </div>
            <div class="form-group col-md-2">
                </div>
            <div class="form-group col-md-2"></div>
        <div class="form-group col-md-8">
            <div class="form-group text-center" style="height:20%; align-items: center;">
                <input type="button" value="1" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="2" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="3" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
            </div>
            <div class="form-group text-center" style="height:20%;">
                <input type="button" value="4" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="5" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="6" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
            </div>
            <div class="form-group text-center" style="height:20%;">
                <input type="button" value="7" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="8" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
                <input type="button" value="9" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
            </div>
            <div class="form-group text-center" style="height:20%;">
                <input type="button" value="0" class="btn btn-primary btn-lg" style="width:30%; height:100%;" onclick="numPadAddNum(this.value)">
            </div>
        </div>
            <div class="form-group col-md-2 col-md-offset-5">
                <div class="form-group col-md-12 " style="height: 20%;">
                </div>
                <div class="form-group col-md-12 " style="height: 20%;">
                    <input type="button" value="Clear" class="btn btn-primary btn-lg" style="width: 100%; height: 100%;" onclick="numPadClear()">
                </div>
                <div class="form-group col-md-12 " style="height: 20%;">
                    <input type="button" value="Enter" class="btn btn-primary btn-lg" style="width: 100%; height: 100%;" onclick="showModalForReceipt(document.getElementById('<%=txtOtherAmount.ClientID%>').value)">
                </div>
            </div>
        </div>
    </asp:Panel>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
   
</asp:Content>
