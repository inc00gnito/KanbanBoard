// popup window for adding cards
$(function () {

    var placeHolderElement = $("#modalCard");
    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data("url");
        $.get(url).done(function (data) {
            placeHolderElement.html(data);
            placeHolderElement.find(".modal").modal("show");
        });
    });
    placeHolderElement.on('click',
        '[data-save="modal"]',
        function (event) {
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var sendData = form.serialize();
            $.post(actionUrl, sendData).done(function (data) {
                placeHolderElement.find('.modal').modal('hide');
            });
        });
});
$(function() {
    $("#addCardPlaceHolder").on('hidden.bs.modal',
        function() {
            location.reload();
        });
});

// sortable columns
$(function () {
    $(".columnsBody").sortable({
        placeholder: "ui-state-highlight"
    });
    $(".columnsBody").sortable({
        handle: "div.card-header",
        cancel: "div.card-body"
    });
    $(".columnsBody").disableSelection();
   /* $(".columnsBody").sortable('refresh');*/

});
//$(function() {
//    $("#draggable").draggable({
//        zIndex:100
//    });
//    $("#droppable").droppable({
//        accept: ("#draggable"),
//        drop: function(event, ui) {
//            $(this)
//        }
//    })
//})