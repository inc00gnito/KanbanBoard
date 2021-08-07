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
    $("#modalCard").on('hidden.bs.modal',
        function() {
            location.reload();
        });
});

 //sortable columns
$(function () {
    $(".columnsBody").sortable({
        forcePlaceholderSize: true,
        placeholder: "ui-state-highlight",
        handle: "div.card-header",
        cancel: "div.card-body",
        update: function (event, ui) {
            $(this).children().each(function (index) {
                if ($(this).attr('data-position') != (index + 1)) {
                    $(this).attr('data-position', index + 1).addClass('updated');
                }
            });
            var positions = new Array();

            $('.updated').each(function () {
                var columnPosition = {
                    columnId: $(this).attr('data-id'),
                    positionId: $(this).attr('data-position')
                }
                positions.push(columnPosition);
                $(this).removeClass(".updated");
            });
            positions = JSON.stringify( positions );
            $.ajax({
                type: "POST",
                url: "/api/ColumnPosition/columnupdate",
                data: positions,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                
            });

        }
    });
    $(".columnsBody").sortable('refresh');
});
// sortable cards within columns
$(function () {
    $(".columnDroppable").sortable({
        forcePlaceholderSize: true,
        connectWith: ".columnDroppable",
        placeholder: "ui-state-highlight",
       
    });
    //getter 
    var connectWith = $(".columnDroppable").sortable("option", "connectWith");
    // Setter
    $(".columnDroppable").sortable("option", "connectWith", ".columnDroppable");
});
