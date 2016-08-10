<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TryOurBeta.ascx.cs" Inherits="ReferLocals.UserControl.TryOurBeta" %>
<div class="input-group input-group-lg input-large" style="width: 100% !important">
    <asp:TextBox type="text" id="txtEmail" runat="server" CssClass="form-control" placeholder="Enter E-mail"></asp:TextBox>
    <span class="input-group-btn">
        <asp:Button ID="btnTryOurBeta" runat="server" CssClass="btn green" style="text-transform: capitalize; font-size: 16px;" Text="Try our beta site" OnClick="btnTryOurBeta_Click" />
    </span>
</div>
      
<div class="ccyy" style="margin:15px 0 35px 1px; float:left; width:100%;">
        <div class="icheck-list">
          <label style="color:#fff;">
            <input type="checkbox" id="chkIsProfession" runat="server" class="icheck" data-checkbox="icheckbox_flat-grey">
            I am a Professional and would like to know when your App is coming out </label>
        </div>
      </div>