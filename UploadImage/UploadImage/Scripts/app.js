﻿jQuery(document).ready(function () {

    "use strict";
    var $ = jQuery.noConflict();

    $("#input-id").fileinput({
        uploadUrl: "http://localhost:65125/api/FileUpload", // server upload action
        uploadAsync: true,
        maxFileCount: 5
    });
});