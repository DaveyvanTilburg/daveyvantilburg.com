$(function () {
    $('#saveLoadConsole').val('');

    $.get('/ExportAnything/Types', function (result) {
        let types = eval(result);
        types.forEach(function (t) {
            $('#divTypeOptions').append(`<div><input id="type${t}" type="radio" name="typeSelection" value="${t}"><label for="type${t}">${t.charAt(0).toUpperCase() + t.slice(1) }</label></div>`);
        });
    });

    $(document).on('click', '#saveLoadToggle', function (e) {
        e.preventDefault();
        $('#saveLoadWrapper').toggleClass('show');
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

        try {
            let deserialized = JSON.parse(serialized);

            $('input[name=typeSelection]').prop('checked', false);
            $(`input[name=typeSelection][value=${deserialized.type}]`).prop('checked', true);

            $('.column-selections .column-selection:not(.header)').remove();

            deserialized.columns.forEach(function (column) {
                let isChecked = column.alias !== undefined;
                let checkedProperty = isChecked ? ' checked="checked"' : '';
                let div = $(`<div id="${column.name}" class="column-selection dropzone"><div><input class="column" type="checkbox" name="columns" id="col${column.name}" value="${column.name}"${checkedProperty}}><label for="id="col${column.name}"" draggable="true">${column.name}</label></div></div>`);

                if (isChecked) {
                    div.addClass('output');
                    div.append(`<div><input class="alias" type="text" name="aliases" value="${column.alias}" /></div>`);
                    div.append(`<div><input class="index" type="text" name="indexes" value="0" readonly="readonly" /></div>`);
                }

                $('.column-selections').append(div);
            });

            $('#divPhaseOne').css('display', 'none');
            $('#divPhaseTwo').css('display', 'flex');

            updateAllIndexes();
        } catch (error) {
            console.log(error);
        }

        return false;
    });

    $(document).on('click', '#divSubmitOne', function (e) {
        e.preventDefault();

        let type = objectType();

        if (type === undefined || type == '')
            return false;

        $.get(`/ExportAnything/TypeDefinition?typeSelection=${type}`, function (result) {
            let typeDefinition = eval(result);

            typeDefinition.forEach(function (p) {
                $('.column-selections').append(`<div id="${p}" class="column-selection dropzone"><div><input class="column" type="checkbox" name="columns" id="col${p}" value="${p}"><label for="col${p}" draggable="true">${p}</label><div></div>`);
            });

            $('#divPhaseOne').css('display', 'none');
            $('#divPhaseTwo').css('display', 'flex');
        });

        return false;
    });


    $(document).on('change', '.column', function () {
        let checked = $(this).is(':checked');
        let div = $(this).closest('.column-selection');

        if (checked) {
            div.addClass('output');
            div.append(`<div><input class="alias" type="text" name="aliases" value="${$(this).val()}" /></div>`);
            div.append(`<div><input class="index" type="text" name="indexes" value="0" readonly="readonly" /></div>`);
        } else {
            div.removeClass('output');
            div.find('.alias').parent().remove();
            div.find('.index').parent().remove();
        }
        updateAllIndexes();
    });

    new Draggables().init();
});

function objectType() {
    return $('input[name=typeSelection]:checked').val();
}

function columns() {
    let columns = $('.column-selection:not(.header)');

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