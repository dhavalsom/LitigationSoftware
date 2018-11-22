$(".imgLitigationDD").click(function (e) {
    showHideControls($(this));
});

$(".imgLitigationDDSave").click(function (e) {
    var txtControl = null;
    var itSectionDDControl = null;
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
    $('.ddITSectionCategory').each(function () {
        itSectionDDControl = $(this);
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
    //code specific to ITReturn details
    if (itSectionDDControl != null) {
        item.IsReturn = itSectionDDControl.val() == "1"; //i.e. if it is ROI
        item.SectionCategoryId = itSectionDDControl.val();
        refreshActionName += "?categoryId=" + item.SectionCategoryId;
        $(".ddLitigationDD").change(function (e) {
            window.location = '/TaxReturn/GetITReturnDetails?fyayId='
                + $('#ITReturnDetailsObject_FYAYID').val()
                + '&itsectionid=' + $('#ITReturnDetailsObject_ITSectionID').val()
                + '&itsectioncategoryid=' + $('#ITReturnDetailsObject_ITSectionCategoryID').val();
        });
    }
    $.ajax({
        type: 'POST',
        url: '/' + addActionName + '/',
        data: JSON.stringify(item),
        success: function (data) {
            $.ajax({
                type: 'GET',
                url: '/' + refreshActionName,
                success: function (refreshedData) {
                    refreshOptions(ddControl, refreshedData, txtControl.val());
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

function refreshOptions(ddControl, list, defaultVal) {
    ddControl.empty(); 
    $.each(list, function (val, obj) {
        ddControl.append(
            $('<option' + (obj.Description == defaultVal ? ' selected' : '')+ '></option>').val(val).html(obj.Description)
        );
    });
}

$(".imgLitigationDDClose").click(function (e) {
    showHideControls($(this));
});

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
    control.closest(".custom-dd").find('.imgLitigationDDClose').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
}
