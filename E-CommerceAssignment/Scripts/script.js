var i = 0;
$(document).ready(function () {

    $(window).on('scroll', function () {
        $('nav').toggleClass('nav-hide', $(window).scrollTop() > i);
        i = $(window).scrollTop();
        if ($(window).scrollTop()) {
            $('nav').addClass('nav-show');
        } else {
            $('nav').removeClass('nav-show');
        }
    });
    $('#search-btn').on('click', function () {
        $('.search-container').addClass('search-bar-show');
    });
    $('.close-search').on('click', function () {
        $('.search-container').removeClass('search-bar-show');
    });
});

$(window).on("load", function () {
    $(".loading").delay(500).fadeOut("slow");
});
// multiple handled with value 
var pmdSliderValueRange = document.getElementById('pmd-slider-value-range');

noUiSlider.create(pmdSliderValueRange, {
    start: [500, 4000], // Handle start position
    connect: true, // Display a colored bar between the handles
    tooltips: [wNumb({ decimals: 0 }), wNumb({ decimals: 0 })],
    format: wNumb({
        decimals: 0,
        thousand: '',
        postfix: '',

    }),
    range: { // Slider can select '0' to '9999'
        'min': 0,
        'max': 5000
    },
});

var valueMax = document.getElementById('value-max'),
    valueMin = document.getElementById('value-min');

// When the slider value changes, update the input and span
pmdSliderValueRange.noUiSlider.on('update', function (values, handle) {
    if (handle) {
        valueMax.innerText = '$' + values[handle];
        console.log(values)
        $.ajax({
            url: 'Product/FilterByPrice',
            type: 'POST',
            data: { min: values[0], max: values[1] },
            beforeSend: function () {
                $('#wating').delay(1000).show(0)
            },
            complete: function () {
                $('#wating').hide()
            },
            success: function (data) {
                var resultCount = '';
                var result = '';
                $.each(data, function (index, value) {
                    result += '<a href="/Product/EditProduct/' + value.Id + '" class="item"><div id="slider">'
                    $.each(value.Photos, function (index, item) {
                        result += '<div class="item-img"><img src = "' + item.Path + '/' + item.Title + '" alt = "' + item.Title+ '" /></div>'
                    })
                    result += `</div><div class="item-detail"><h5>` + value.Name + `</h5 ><h5>` + value.Storage + `</h5><div class="price"><p>$` + value.Price + `</p><p>$1799</p></div></div></a>`
                    
                
                })
                $('.product-list-items').html(result)
                resultCount += '<p>Showing result 1 - ' + data.length + '</p>'
                $('#resultCount').html(resultCount)
            }
        })
    } else {
        valueMin.innerText = '$' + values[handle];
    }
});	
