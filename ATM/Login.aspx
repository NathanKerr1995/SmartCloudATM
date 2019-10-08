<%@ Page Title="" Language="C#" MasterPageFile="~/MainStyle.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ATM.Login" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript"> 
        function numPadAddNum(num) {
            if (document.getElementById('<%= txtNumPad.ClientID %>').value.length <= 3) {
                var txt = document.getElementById('<%= txtNumPad.ClientID %>').value;
                txt = txt + num;
                document.getElementById('<%= txtNumPad.ClientID %>').value = txt;
                $("#ContentPlaceHolder1_txtNumPad").focusTextToEnd();
            }
            else {
                $("#ContentPlaceHolder1_txtNumPad").focusTextToEnd();
            }
        }

        function numPadClear() {
            document.getElementById('<%= txtNumPad.ClientID %>').value = null;
            $("#ContentPlaceHolder1_txtNumPad").focusTextToEnd();
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
            $("#ContentPlaceHolder1_txtNumPad").inputFilter(function (value) {
                return /^\d*$/.test(value) && (value === "" || value.length <= 4);
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
    <asp:Panel ID="pnlNumberButtons" runat="server" style="width:100%; height:90%;">
        <div class="form-row" style="width:100%; height:100%;">
            <div class="form-group col-md-2">
                </div>
            <div class="form-group col-md-8 text-center login-textbox-outer" >
                <div class="login-textbox-inner">
                    <h2>
                        <asp:Label ID="lblResponse" runat="server" CssClass="text-white font-weight-bold" Text="Enter Pin"></asp:Label>
                        </h2>
            <asp:TextBox ID="txtNumPad" runat="server" CssClass="form-control" TextMode="Password" Width="90%" Font-Size="XX-Large"></asp:TextBox>
                </div>
                </div>
            <div class="form-group col-md-2">
                <asp:LinkButton ID="lnbtSpeak" CssClass="btn-lg float-right" runat="server" OnClick="lnbtSpeak_Click" Font-Size="XX-Large"><i class='fa fa-volume-up'></i></asp:LinkButton>
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
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary btn-lg" Style="width: 100%; height: 100%;" Text="Cancel" OnClick="btnCancel_Click" UseSubmitBehavior="False"  />
                </div>
                <div class="form-group col-md-12 " style="height: 20%;">
                    <input type="button" value="Clear" class="btn btn-primary btn-lg" style="width: 100%; height: 100%;" onclick="numPadClear()">
                </div>
                <div class="form-group col-md-12 " style="height: 20%;">
                    <asp:Button ID="btnNumPadEnter" runat="server" class="btn btn-primary btn-lg" Style="width: 100%; height: 100%;" Text="Enter" OnClick="btnNumPadEnter_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
