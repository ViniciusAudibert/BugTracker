'use strict';

function BugTrackerModel() {
    this.urlGetPagined = '/BugTracker/GetBugTrackerPaginedByApp';
    this.urlCount = '/BugTracker/GetCountBugTrackerByApp';
    this.urlGetGraphics = '/BugTracker/GetGraphicModelByIdApplication';
}

BugTrackerModel.prototype.getPagined = function (filter) {
    return $.ajax({
        url: this.urlGetPagined,
        type: "POST",
        data: { filter: filter}
    });
}

BugTrackerModel.prototype.countBugs = function (filter) {
    return $.ajax({
        url: this.urlCount,
        type: "POST",
        data: {filter: filter}
    });
}

BugTrackerModel.prototype.getGraphics = function (idApplication, filters) {
    return $.ajax({
        url: this.urlGetGraphics,
        data: { idApplication: idApplication, filters: filters }
    });
}