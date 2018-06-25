$(".imgLitigationDD").click(function (e) {
    showHideControls($(this));
});

$(".imgLitigationDDSave").click(function (e) {
    var txtControl = null;
    var ddControl = null;
    var saveButton = $(this);
    var addActionName = "", refreshActionName = "";
    $(e.target).closest(".custom-dd").find('.ddLitigationDD').each(function () {
        ddControl = $(this);
    });
    $(e.target).closest(".custom-dd").find('.txtLitigationDD').each(function () {
        txtControl = $(this);
    });
    $(e.target).closest(".custom-dd").find('.hdActionNameLitigationDD').each(function () {
        addActionName = $(this).val();
    });
    $(e.target).closest(".custom-dd").find('.hdRefreshActionNameLitigationDD').each(function () {
        refreshActionName = $(this).val();
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
    item.IsDefault = false;
    item.Active = true;
    
    $.ajax({
        type: 'POST',
        url: '/' + addActionName + '/',
        data: JSON.stringify(item),
        success: function (data) {
            $.ajax({
                type: 'GET',
                url: '/' + refreshActionName + '/',
                success: function (refreshedData) {
                    refreshOprons(ddControl, refreshedData, txtControl.val());
                    showHideControls(saveButton);
                },
                contentType: "application/json",
                dataType: 'json'
            });
        },
        contentType: "application/json",
        dataType: 'json'
    });
});

function refreshOprons(ddControl, list, defaultVal) {
    ddControl.empty(); 
    $.each(list, function (val, obj) {
        ddControl.append(
            $('<option' + (obj.Description == defaultVal ? ' selected' : '')+ '></option>').val(val).html(obj.Description)
        );
    });
}
function showHideControls(control) {
    control.css("display", "none");
    var isAddMode = control.hasClass("imgLitigationDD"); //in add mode and switch to the save mode now
    var isSaveMode = !isAddMode;

    control.closest(".custom-dd").find('.imgLitigationDD').each(function () {
        $(this).css("display", isAddMode ? "none" : "inline");
    });

    control.closest(".custom-dd").find('.imgLitigationDDSave').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-dd").find('.txtLitigationDD').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
}
