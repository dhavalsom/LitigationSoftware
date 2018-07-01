var templates =
{
 trSubHeadItem: function () {/*
    <tr>
        <td></td>
        <td></td>
        <td>{Description}:-</td>
        <td>
            <input id="txtITSubHead_{ITSubHeadId}" name="txtITSubHead_{ITSubHeadId}" type="text" value="">
        </td>
    </tr>
	*/}.toString().slice(14, -3)
}

$(".imgSubHead").click(function (e) {
    showHideSubHeadControls($(this));
});

$(".imgSubHeadSave").click(function (e) {
    var txtControl = null;
    var chkControl = null;
    var saveButton = $(this);
    var itHeadId = 0;
    $(e.target).closest(".custom-sub-head").find('.chkSubHead').each(function () {
        chkControl = $(this);
    });
    $(e.target).closest(".custom-sub-head").find('.txtSubHead').each(function () {
        txtControl = $(this);
    });
    $(e.target).closest(".custom-sub-head").find('.hdIIHeadIdSubHead').each(function () {
        itHeadId = $(this).val();
    });
    if (txtControl.val() == "") {
        txtControl.addClass("validationError");
        return;
    }
    else {
        txtControl.removeClass("validationError");
    }

    var item = {};
    item.Description = txtControl.val();
    item.IsAllowance = chkControl.prop("checked");
    item.Active = true;
    item.ITHeadId = itHeadId;
    
    $.ajax({
        type: 'POST',
        url: '/manageITSubHead/',
        data: JSON.stringify(item),
        success: function (data) {
            var trHtml = templates.trSubHeadItem
                .replace("{ITHeadId}", itHeadId)
                .replace("{ITSubHeadId}", data.Id)
                .replace("{ITSubHeadId}", data.Id)
                .replace("{Description}", item.Description);
            $.each($('.tblITRetDetails tr.' + (item.IsAllowance ? 'trAllowance' : 'trDisallownace') + itHeadId), function (val, obj) {
                $(this).after(trHtml);
            });            
        },
        contentType: "application/json",
        dataType: 'json'
    });
});

function showHideSubHeadControls(control) {
    control.css("display", "none");
    var isAddMode = control.hasClass("imgSubHead"); //in add mode and switch to the save mode now
    var isSaveMode = !isAddMode;

    control.closest(".custom-sub-head").find('.imgSubHead').each(function () {
        $(this).css("display", isAddMode ? "none" : "inline");
    });

    control.closest(".custom-sub-head").find('.imgSubHeadSave').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-head").find('.txtSubHead').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-head").find('.chkSubHead').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
}