﻿var promise = require('es6-promise'),
    gulp = require('gulp'),
    watch = require('gulp-watch'),
    del = require('del'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    merge = require('merge-stream'),
    order = require('gulp-order'),
    less = require('gulp-less'),
    cleanCSS = require('gulp-clean-css'),
    autoprefixer = require('gulp-autoprefixer'),
    rename = require('gulp-rename');

//clear all files
function clear() {
    del([
        'content/dist/css/**/*.css',
        'content/dist/js/**/*.js',
        'content/dist/fonts/**/*'
    ]);
}
function clearAdmin() {
    del([
        'Areas/Admin/content/dist/css/**/*.css',
        'Areas/Admin/content/dist/js/**/*.js',
        'Areas/Admin/content/dist/fonts/**/*'
    ]);
}


//create css files
function createCss() {
    var lessStream = gulp.src([
      'less/site.less'
    ])
        .pipe(less())
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
        .pipe(concat('sitecss.css'));

    var cssStream = gulp.src([
        'bower_components/bootstrap/dist/css/bootstrap.min.css',
        'bower_components/bootstrap/dist/css/bootstrap-theme.min.css',
        'bower_components/font-awesome/css/font-awesome.min.css'
    ])
        .pipe(concat('css.css'));

    var mergedStream = merge(lessStream, cssStream)
        .pipe(order([
            'css.css',
            'sitecss.css'
        ]))
            .pipe(concat('site.min.css'))
            .pipe(gulp.dest('content/dist/css'));

    ////by one less
    //gulp.src([
    //  'less/login-page.less'
    //])
    //    .pipe(less())
    //    .pipe(cleanCSS({ compatibility: 'ie8' }))
    //    .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
    //    .pipe(rename({
    //        suffix: '.min'
    //    }))
    //    .pipe(gulp.dest('content/dist/css'));

    //by one css
    //gulp.src([

    //])
    //    .pipe(gulp.dest('content/dist/css'));
}
function createCssAdmin() {
    var lessStream = gulp.src([
       'Areas/Admin/less/site.less',
       'Areas/Admin/less/sx-gv.less',
       'Areas/Admin/less/sx-gvl.less'
    ])
        .pipe(less())
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
        .pipe(concat('sitecss.css'));

    var cssStream = gulp.src([
        'bower_components/bootstrap/dist/css/bootstrap.min.css',
        'bower_components/bootstrap/dist/css/bootstrap-theme.min.css',
        'bower_components/font-awesome/css/font-awesome.min.css',
        'bower_components/metisMenu/dist/metisMenu.min.css'
    ])
        .pipe(concat('css.css'));

    var mergedStream = merge(lessStream, cssStream)
        .pipe(order([
            'css.css',
            'sitecss.css'
        ]))
            .pipe(concat('site.min.css'))
            .pipe(gulp.dest('Areas/Admin/content/dist/css'));

    ////by one less
    //gulp.src([
    //   'less/login.less',
    //   'less/grid-users.less',
    //   'less/grid-pictures.less',
    //   'less/grid-banners.less',
    //   'less/ps-tree.less',
    //   'less/grid-a-authors.less',
    //   'less/grid-aphorisms.less',
    //   'less/test-matrix.less',
    //   'less/chat.less'
    //])
    //    .pipe(less())
    //    .pipe(cleanCSS({ compatibility: 'ie8' }))
    //    .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
    //    .pipe(rename({
    //        suffix: '.min'
    //    }))
    //    .pipe(gulp.dest('content/dist/css'));

    //by one css
    gulp.src([
       'bower_components/lightbox2/dist/css/lightbox.min.css',
       'bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css',
       'bower_components/morris.js/morris.css'
    ])
        .pipe(gulp.dest('Areas/Admin/content/dist/css'));
}

//create fonts
function createFonts() {
    gulp.src([
        'bower_components/font-awesome/fonts/**/*'
    ])
        .pipe(gulp.dest('content/dist/fonts'));
}
function createFontsAdmin() {
    gulp.src([
        'bower_components/font-awesome/fonts/**/*'
    ])
        .pipe(gulp.dest('Areas/Admin/content/dist/fonts'));
}

//create js files
function createJs() {
    var js = gulp.src([
        'bower_components/jquery/dist/jquery.min.js',
        'bower_components/bootstrap/dist/js/bootstrap.min.js'
    ])
        .pipe(concat('js.js'));

    var sitejs = gulp.src([
        'scripts/site.js'
    ])
        .pipe(uglify())
        .pipe(concat('sitejs.js'));

    var mergedStream = merge(js, sitejs)
        .pipe(order([
            'js.js',
            'sitejs.js'
        ]))
            .pipe(concat('site.min.js'))
            .pipe(gulp.dest('content/dist/js'));

    //by one js
    gulp.src([
        'bower_components/jquery-validation/dist/jquery.validate.min.js',
        'bower_components/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.min.js',
        'bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js'
    ])
        .pipe(gulp.dest('content/dist/js'));

}
function createJsAdmin() {
    var js = gulp.src([
        'bower_components/jquery/dist/jquery.min.js',
        'bower_components/bootstrap/dist/js/bootstrap.min.js',
        'bower_components/metisMenu/dist/metisMenu.min.js'
    ])
        .pipe(concat('js.js'));

    var sitejs = gulp.src([
        'Areas/Admin/scripts/site.js',
        'Areas/Admin/scripts/sx-gv.js',
        'Areas/Admin/scripts/sx-gvl.js'
    ])
        .pipe(uglify())
        .pipe(concat('sitejs.js'));

    var mergedStream = merge(js, sitejs)
        .pipe(order([
            'js.js',
            'sitejs.js'
        ]))
            .pipe(concat('site.min.js'))
            .pipe(gulp.dest('Areas/Admin/content/dist/js'));

    //by one js
    gulp.src([
        'bower_components/jquery-validation/dist/jquery.validate.min.js',
        'bower_components/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.min.js',
        'bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js',
        'bower_components/lightbox2/dist/js/lightbox.min.js',
        'bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js',
        'bower_components/moment/min/moment-with-locales.min.js',
        //'bower_components/signalr/jquery.signalR.min.js',
        'bower_components/raphael/raphael.min.js',
        'bower_components/morris.js/morris.min.js'
    ])
        .pipe(gulp.dest('Areas/Admin/content/dist/js'));

}

function createImagesAdmin() {
    gulp.src([
        'bower_components/lightbox2/dist/images/**'
    ])
    .pipe(gulp.dest('Areas/Admin/content/dist/images'));
}

gulp.task('watch', function (cb) {
    watch([
        'less/**/*.less',
        'scripts/**/*.js'
    ], function () {
        clear();
        createCss();
        createFonts();
        createJs();
    });
});
gulp.task('watchAdmin', function (cb) {
    watch([
        'Areas/Admin/less/**/*.less',
        'Areas/Admin/scripts/**/*.js'
    ], function () {
        clearAdmin();
        createCssAdmin();
        createFontsAdmin();
        createJsAdmin();
        createImagesAdmin();
    });
});