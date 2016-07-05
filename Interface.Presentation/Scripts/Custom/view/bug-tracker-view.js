'use strict';

function BugTrackerView() {
    this.bModel = new BugTrackerModel();
    this.win = $(window);
    this.filters = { idApplication: idApplication, Page: 0, Limit: 30 };
}

BugTrackerView.prototype.init = function () {
    self = (this);

    this.renderGraphics();
    this.loadData();
    this.showCountBugs();
    
    this.win.scroll(function () {
        if ($(document).height() - self.win.height() === self.win.scrollTop())
            self.loadData();
    });

    $('#filter').click(function () {
        self.filters.Page = 0;
        $('#list-bug-tracks').empty();
        $('#count-bugs').empty();

       self.filters.Trace = $('#Trace').val();
       self.filters.Status = $('input[name=Status]:checked').map(function(){
                                   return $(this).val();
                               }).get();
        self.loadData();
        self.showCountBugs();
    });

    $('table').on('click', '.trace-link', function () {
        $('#trace-content').html($(this).attr('data-value'));
    });
};


BugTrackerView.prototype.loadData = function () {
    self = (this);
    self.filters.Page += 1;
    this.bModel.getPagined(self.filters).done(function (data) {
        $('#list-bug-tracks').append(data);
    });
};

BugTrackerView.prototype.showCountBugs = function () {
    this.bModel.countBugs(self.filters).done(function (data) {
        for (var i in data.data) {
            $('#count-bugs').append($('<span>').addClass("fa fa-exclamation-triangle " + data.status[data.data[i].Status - 1]).html(data.data[i].Count));
        }
    });
};

BugTrackerView.prototype.renderGraphics = function () {
    this.bModel.getGraphics(idApplication).done(function (data) {

        var dataGraphicFormat = $.map(data.data, function (e) {
            return { name: data.status[e.Status - 1], y: e.Count };
        });

        if (data.data.length > 0) {
            $('#graphics').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Last 7 days'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f} %</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y:.0f}',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Brands',
                    colorByPoint: true,
                    data: dataGraphicFormat
                }   ]
            });
        } else {
            $('#graphics').hide();
        }
    });
};

