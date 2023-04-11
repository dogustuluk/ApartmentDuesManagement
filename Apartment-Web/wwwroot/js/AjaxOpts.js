//sadece apartmanlarin isimlerini doner
$().ready(function () {
    $("select[class*='ajaxApartment']").change(function () {
        var url = "/_Base/GetAjax_DDL";
        var ddl = $(this);
        $.getJSON(url, { WHAT: 'Apartments', Params: '', ID: ddl.val() }, function (data) {
            var items = '';
            $("select[class*='ajaxApartment']").empty();
            $.each(data, function (i, data) {
                items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
            });
            $("select[class*='ajaxApartment']").html(items);
        });
    });
});



//$.getJSON(url, { WHAT: 'Apartments', Params: '', ID: ddl.val(), orderBy: 'ApartmentName' }, function (data) {
//    // Verileri sırala
//    data.sort(function (a, b) {
//        var nameA = a.ApartmentName.toUpperCase();
//        var nameB = b.ApartmentName.toUpperCase();
//        if (nameA < nameB) {
//            return -1;
//        }
//        if (nameA > nameB) {
//            return 1;
//        }
//        return 0;
//    });
//    // Verileri sayfaya yansıt
//    var items = '';
//    $.each(data, function (i, data) {
//        items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
//    });
//    $("select[class*='ajaxApartment']").html(items);
//});
