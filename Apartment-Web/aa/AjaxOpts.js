//-------------------- Genel Fonksiyonlar
$().ready(function () {
    $("select[class*='ajaxCity']").change(function () {
        var url = "/_Base/GetAjax_DDL";
        var ddl = $("select[class*='ajaxCity']");
        var Params = ddl.attr("Params");
        $.getJSON(url, { WHAT: 'Counties', Params: '', ID: ddl.val() }, function (data) {
            var items = '';
            $("select[class*='ajaxCounty']").empty();
            $.each(data, function (i, data) {
                items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
            });
            $("select[class*='ajaxCounty']").html(items);

            if (Params != '') {
                $("select[class*='ajaxDistrict']").empty();
                $("select[class*='ajaxDistrict']").html("<option value='0'>Seçiniz</option>");
            }
        });
    });
    $("select[class*='ajaxCounty']").change(function ()
    {    
        var ddl = $("select[class*='ajaxCounty']");
        var Params = ddl.attr("Params");
        if (Params != '') {
            var url = "/_Base/GetAjax_DDL";
            $.getJSON(url, { WHAT: 'Districts', Params: '', ID: ddl.val() }, function (data) {
                var items = '';
                $("select[class*='ajaxDistrict']").empty();
                $.each(data, function (i, data) {
                    items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                });
                $("select[class*='ajaxDistrict']").html(items);
            });
        }
    });
    $("select[class*='ajaxParentJobTypeID']").change(function () {
        var ddl = $("select[class*='ajaxParentJobTypeID']");
        var Params = ddl.attr("Params");
        if (Params != '') {
            var url = "/_Base/GetAjax_DDL";
            $.getJSON(url, { WHAT: 'JobTypes', Params: '', ID: ddl.val() }, function (data) {
                var items = '';
                $("select[class*='ajaxJobTypeID']").empty();
                $.each(data, function (i, data) {
                    items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                });
                $("select[class*='ajaxJobTypeID']").html(items);
            });
        }
    });
    $("select[class*='ajaxSegmentID1']").change(function () {
        var ddl = $("select[class*='ajaxSegmentID1']");
        var Params = ddl.attr("Params");
        var url = "/_Base/GetAjax_DDL";
        $.getJSON(url, { WHAT: 'Segments', Params: '', ID: ddl.val() }, function (data) {
            var items = '';
            $("select[class*='ajaxSegmentID2']").empty();
            $.each(data, function (i, data) {
                items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
            });
            $("select[class*='ajaxSegmentID2']").html(items);
        });
    });
    $("select[class*='ajaxCompanyID']").change(function () {
        var ddl = $("select[class*='ajaxCompanyID']");
        var Params = ddl.attr("Params");
        var url = "/_Base/GetAjax_DDL";
        $.getJSON(url, { WHAT: 'Company', Params: Params, ID: ddl.val() }, function (data) {
            var items = '';
            if (Params = 'Project') {
                $("select[class*='ajaxProjectID']").empty();
                $.each(data, function (i, data) {
                    items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                });
                $("select[class*='ajaxProjectID']").html(items);
            }
            else if (Params = 'Survey') {
                $("select[class*='ajaxSurveyID']").empty();
                $.each(data, function (i, data) {
                    items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                });
                $("select[class*='ajaxSurveyID']").html(items);
            }
            else if (Params = 'Payment') {
                $("select[class*='ajaxPaymentID']").empty();
                $.each(data, function (i, data) {
                    items += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                });
                $("select[class*='ajaxPaymentID']").html(items);
            }
        });
    });
});

