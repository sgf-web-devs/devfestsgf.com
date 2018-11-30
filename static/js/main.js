$(function() {
    $('.presentations li').on('click', function () {
        var parent = $('body');
        var index = $(this).index();

        if (parent.hasClass('detail_active')) {
            deActivateDescription();
            return;
        }
        
        $(parent).addClass('detail_active');
        $('.talk_detail').removeClass('active');
        $('.talk_detail:eq('+ index +')').addClass('active');
    });

    function deActivateDescription() {
        $('body').removeClass('detail_active');
    }

    $('.talk_detail').on('click', function(e) {
        e.stopPropagation();
    });

    $('#talk_details').on('click', function (e) {
        $('body').removeClass('detail_active');
    });

    $('.close').on('click', function () {
        deActivateDescription();
    });
});