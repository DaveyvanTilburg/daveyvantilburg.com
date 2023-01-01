$(function () {
    $('#saveLoadConsole').val('');

    $(document).on('click', '#saveLoadToggle', function (e) {
        e.preventDefault();
        $('#saveLoadWrapper').toggle();
        return false;
    });

    $(document).on('click', '#saveButton', function (e) {
        e.preventDefault();

        let composed = {
            type: objectType(),
            columns: columns()
        }

        let serialized = JSON.stringify(composed);
        $('#saveLoadConsole').val(serialized);

        return false;
    });

    $(document).on('click', '#loadButton', function (e) {
        e.preventDefault();

        let serialized = $('#saveLoadConsole').val();
        let deserialized = JSON.parse(serialized);

        $('input[name=typeSelection]').prop('checked', false);
        $(`input[name=typeSelection][value=${deserialized.type}]`).prop('checked', true);

        $('.column-selections .column-selection').remove();

        deserialized.columns.forEach(function (column) {
            let isChecked = column.alias !== undefined;
            let checkedProperty = isChecked ? ' checked="checked"' : '';
            let div = $(`<div id="${column.name}" class="column-selection dropzone"><input class="column" type="checkbox" name="columns" value="${column.name}"${checkedProperty}}><span draggable="true">${column.name}</span></div>`);

            if (isChecked) {
                div.addClass('output');
                div.append(`<input class="alias" type="text" name="aliases" value="${column.alias}" />`);
                div.append(`<input class="index" type="text" name="indexes" value="0" readonly="readonly" />`);
            }

            $('.column-selections').append(div);
        });

        $('#divPhaseOne').css('display', 'none');
        $('#divPhaseTwo').css('display', 'block');

        updateAllIndexes();

        return false;
    });

    $.get('/ExportAnything/Types', function (result) {
        let types = eval(result);
        types.forEach(function (t) {
            $('#divTypeOptions').append(`<div><input id="type${t}" type="radio" name="typeSelection" value="${t}"><label for="type${t}">${t}</label></div>`);
        });
    });

    $(document).on('click', '#divSubmitOne', function (e) {
        e.preventDefault();

        let type = objectType();
        $.get(`/ExportAnything/TypeDefinition?typeSelection=${type}`, function (result) {
            let typeDefinition = eval(result);

            typeDefinition.forEach(function (p) {
                $('.column-selections').append(`<div id="${p}" class="column-selection dropzone"><input class="column" type="checkbox" name="columns" value="${p}"><span draggable="true">${p}</span></div>`);
            });

            $('#divPhaseOne').css('display', 'none');
            $('#divPhaseTwo').css('display', 'block');
        });

        return false;
    });


    $(document).on('change', '.column', function () {
        let checked = $(this).is(':checked');
        let div = $(this).closest('.column-selection');

        if (checked) {
            div.addClass('output');
            div.append(`<input class="alias" type="text" name="aliases" value="${$(this).val()}" />`);
            div.append(`<input class="index" type="text" name="indexes" value="0" readonly="readonly" />`);
        } else {
            div.removeClass('output');
            div.find('.alias').remove();
            div.find('.index').remove();
        }
        updateAllIndexes();
    });

    new Draggables().init();
});

function objectType() {
    return $('input[name=typeSelection]:checked').val();
}

function columns() {
    let columns = $('.column-selection');

    let result = columns.map(function () {
        let column = $(this);

        return {
            name: column.find('.column').val(),
            alias: column.find('.alias').val()
        }
    }).toArray();

    return result;
}

function updateAllIndexes() {
    let outputs = $('.column-selection.output');
    outputs.each(function (div) {
        let index = $('.column-selection.output').index(this);
        $(this).find('.index').val(index + 1);
    });
}