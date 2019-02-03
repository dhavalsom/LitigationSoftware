var templates =
{
 trSubHeadItem: function () {/*
    <tr>
        <td></td>
        <td></td>
        <td>{Description}:-</td>
        <td>
            <div class="row col-12">
                <div class="col-md-6">
                    <input id="txtITSubHead_{ITSubHeadId}" name="txtITSubHead_{ITSubHeadId}" type="text" value="">
                </div>
            </div>
        </td>
    </tr>
	*/}.toString().slice(14, -3)
}

$(".imgSubHeadClose").click(function (e) {
    showHideSubHeadControls($(this));
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
    control.closest(".custom-sub-head").find('.imgSubHeadClose').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-head").find('.txtSubHead').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-head").find('.chkSubHead').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-head").find('.chkSubHeadLabel').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
}