function showHideSubDocCategoryControls(control) {
    control.css("display", "none");
    var isAddMode = control.hasClass("imgSubDocCategory"); //in add mode and switch to the save mode now
    var isSaveMode = !isAddMode;
    control.closest(".custom-sub-doc-category").find('.imgSubDocCategory').each(function () {
        $(this).css("display", isAddMode ? "none" : "inline");
    });
    control.closest(".custom-sub-doc-category").find('.imgSubDocCategoryClose').each(function () {
        $(this).css("display", isAddMode ? "inline" : "none");
    });
    control.closest(".custom-sub-doc-category").find('.tr-custom-add').each(function () {
        if (isAddMode) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}