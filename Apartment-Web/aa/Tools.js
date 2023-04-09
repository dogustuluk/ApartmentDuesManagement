document.addEventListener(
    "DOMContentLoaded",
    function () {
        var tooltipTriggerList = [].slice.call(
            document.querySelectorAll('[data-bs-toggle="tooltip"]')
        );
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    },
    false
);


function Get_BaseUrl() {

    var getUrl = window.location;
    var origin = getUrl.origin;
    var baseUrl = getUrl.origin + '/' + getUrl.pathname.split('/')[1];
    if (origin.includes("localhost"))
        (
            baseUrl = getUrl.origin
        );

    return baseUrl;
}
$(function () {
    $(".datepicker").datepicker({
        dateFormat: 'dd.mm.yy',
        weekStart : 1
    });
});
function formSubmitWithPage(page = 1) {
    document.getElementById('pageIndex').value = page;
    $('form').submit();
}
$().ready(function () {

    $('.dropdown-toggle').dropdown();
    
    $('.phoneNumber').mask("500 0000000", { placeholder: "5__ _______" });
    $('.dogrulamaKodu').mask("0 0 0 - 0 0 0", { placeholder: "_ _ _ - _ _ _" });

    $('.fancybox').fancybox(
        {
            clickSlide: 'false',
            iframe: {
                preload: false,
                css: { width: '90%', height: '90%' }
            },
            'titleShow': false,
            'type': 'iframe'
        });
    $('.fancybox-image').fancybox();
    $('.fancybox-inline').fancybox(
        {
            'titleShow': false,
        });
    $('.fancybox-small1').fancybox(
        {
            iframe: {
                preload: false,
                css: { width: '1080px', height: '750px' }
            },
            'titleShow': false,
            'type': 'iframe'
        });
    $('.fancybox-small2').fancybox(
        {
            iframe: {
                preload: false,
                css: { width: '850px', height: '90%' }
            },
            'titleShow': false,
            'type': 'iframe'
        });
    $('.fancybox-small3').fancybox(
        {
            iframe: {
                preload: false,
                css: { width: '750px', height: '600px' }
            },
            'titleShow': false,
            'type': 'iframe'
        });

    // Left Menu
    $('.nav-toggle').click(function (e) {

        e.preventDefault();
        $("html").toggleClass("openNav");
        $(".nav-toggle").toggleClass("active");

    });

    $('.Select2').select2(
        {
            escapeMarkup: function (m) {
                // Do not escape HTML in the select options text
                return m;
            }
        });

    $('.Select2-Autocomplete').select2({
        closeOnSelect: true,
        minimumInputLength: 3,
        ajax: {
            url: '/_Base/Search_Select2',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    prefix: params.term,
                    Param1: $(this).attr("Param1"),
                    TableName: $(this).attr("title")
                };
            },
            processResults: function (data) {
                //alert(JSON.stringify(data))
                return {
                    results: data
                };
            },
            cache: true
        },
        templateResult: function (item) {
            if (item.loading) return item.text;
            return item.text;
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    });

    //burger-open
    $('.burger').on('click', function () {
        $(this).toggleClass('active');
    });

});


//Enter tuşunda işlem yapma
$(document).keypress(
    function (event) {
        if (event.which == '13' && event.target.tagName != 'TEXTAREA') {
            event.preventDefault();
        }
    });

// Fancybox kapat ve filtreleme düğmesini tetikle  -------------------
function closeFancybox() {
    parent.$.fancybox.close();
    parent.$(".btnFilter").trigger('click');
}


//Sadece Rakam girilebilsin
function TextBoxOnlyNumber() {
    $(".TxtNumber").keydown(function (e) {

        // Allow: backspace, delete, tab, escape, enter and ,
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 188]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl+C,
            (e.keyCode === 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl+C,
            (e.keyCode === 86 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl+X,
            (e.keyCode === 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)
        ) {
            if (e.keyCode == 110 || e.keyCode == 188) {
                var value = $(this).val();
                if (value.includes(",")) {
                    e.preventDefault();
                }
            }

            return;
        }

        // Ensure that it is a number and stop the keypress
        if (e.altKey || ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105))) {
            e.preventDefault();
        }
    });

}

//Tetbox büyük harf
String.prototype.turkishToUpper = function () {
    var string = this;
    var letters = { "i": "İ", "ş": "Ş", "ğ": "Ğ", "ü": "Ü", "ö": "Ö", "ç": "Ç", "ı": "I" };
    string = string.replace(/(([iışğüçö]))+/g, function (letter) { return letters[letter]; })
    return string.toUpperCase();
}
function upperCaseF(a) {
    setTimeout(function () {
        //var string = a.value;
        //var letters = { "i": "İ", "ş": "Ş", "ğ": "Ğ", "ü": "Ü", "ö": "Ö", "ç": "Ç", "ı": "I" };
        //string = string.replace(/(([iışğüçö]))+/g, function (letter) { return letters[letter]; })

        a.value = a.value.toLocaleUpperCase('tr-TR');
    }, 1);
}
//Rakamları binlik ayraca göre formatla 1
function FormatCurrency(MyTextbox) {
    MyValue = MyTextbox.value.replace(/\./g, "");
    arr = MyValue.split(",");
    MyValue1 = arr[0];
    MyValue2 = arr.length > 1 ? arr[1] : "";
    MyResult = '';
    k = 0;
    for (var i = MyValue1.length - 1; i >= 0; i--) {
        if (k == 3) {
            MyResult = MyValue1[i] + '.' + MyResult;
            k = 0;
        }
        else {
            MyResult = MyValue1[i] + MyResult;
        }
        k++;
    }
    MyResult = arr.length > 1 ? MyResult + "," + MyValue2 : MyResult;
    MyTextbox.value = MyResult;
    console.log(MyResult);
}

//Rakamları binlik ayraca göre formatla 2
function FormatCurrency2(MyTextbox) {
    var MyValue = MyTextbox.replace(/./g, "");

    MyResult = '';
    k = 0;
    for (var i = MyValue.length - 1; i >= 0; i--) {
        if (k == 3) {
            MyResult = MyValue[i] + '.' + MyResult;
            k = 0;
        }
        else {
            MyResult = MyValue[i] + MyResult;
        }

        k++;
    }

    console.log(MyResult);
}


function getWindowHeight() {
    if (window.self && self.innerHeight) {
        return self.innerHeight;
    };
    if (document.documentElement && document.documentElement.clientHeight) {
        return document.documentElement.clientHeight;
    };
    return 0;
};
function getWindowWidth() {
    if (window.self && self.innerWidth) {
        return self.innerWidth;
    };
    if (document.documentElement && document.documentElement.clientWidth) {
        return document.documentElement.clientWidth;
    };
    return 0;
};

function clearHtmlTag(MyTextBox) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = $('#' + MyTextBox.id).val();
    $('#' + MyTextBox.id).val(tmp.textContent.replace(String.fromCharCode(8220), " ").replace(String.fromCharCode(8221), " "));
}


