/// 
/// 
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    sass = require('gulp-sass'),
    cleanCss = require('gulp-clean-css'),
    jshint = require('gulp-jshint'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    concat = require('gulp-concat'),
    notify = require('gulp-notify'),
    plumber = require('gulp-plumber'),
    del = require('del');
    //project = require('./project.json');


var siteStaticPath = "../Web/",
    siteStaticScriptsPath = siteStaticPath + "Content/scripts",
    siteStaticCssPath = siteStaticPath + "Content/css";

//Styles
gulp.task('sass', function () {
    gulp.src('wwwroot/src/styles/styles.scss')
        .pipe(sass().on('error', function (err) {
            console.log(err);
            this.emit('end');
        }))
        .pipe(gulp.dest(siteStaticCssPath))
        .pipe(rename({ suffix: '.min' }))
        //.pipe(cleanCss())
        .pipe(gulp.dest(siteStaticCssPath))
        .pipe(notify({ message: 'Styles task complete' }));
});

//gulp.task('minifyCss', function () {
//    gulp.src('wwwroot/src/css/**/*.css')
//        .pipe(concat('vendors-styles.css'))
//        .pipe(gulp.dest(siteStaticCssPath + "/vendor"))
//        .pipe(rename({ suffix: '.min' }))
//        .pipe(minifyCss())
//        .pipe(gulp.dest(siteStaticCssPath + "/vendor"))
//        .pipe(notify({ message: 'MinifyCss task complete' }));
//});

//gulp.task('scripts', function () {
//    return gulp.src([
//        'wwwroot/src/scripts/app.js'
//    ])
//        .pipe(plumber())
//        .pipe(jshint())
//        .pipe(concat('scripts.js'))
//        .pipe(gulp.dest(siteStaticScriptsPath))
//        .pipe(rename({ suffix: '.min' }))
//        .pipe(uglify())
//        .pipe(gulp.dest(siteStaticScriptsPath))
//        .pipe(notify({ message: 'Script bundle task complete' }));
//});

gulp.task('vendor-scripts', function () {
    return gulp.src([
        'wwwroot/lib/jquery/dist/jquery.js',
        'wwwroot/lib/foundation-sites/dist/foundation.js',
        'wwwroot/lib/foundation-sites/js/foundation.reveal.js',
        'wwwroot/lib/foundation-sites/js/foundation.dropdown.js',
        'wwwroot/lib/foundation-sites/js/foundation.interchange.js',
        'wwwroot/lib/foundation-sites/js/foundation.equalizer.js',
        'wwwroot/lib/vue/dist/vue.js'

        
        //'wwwroot/lib/moxie/bin/js/moxie.js',
        //'wwwroot/lib/cookie/cookie.js'
        ])
        .pipe(plumber())
        .pipe(concat('vendors.js'))
        .pipe(gulp.dest(siteStaticScriptsPath))
        .pipe(rename({ suffix: '.min' }))
        .pipe(uglify())
        .pipe(gulp.dest(siteStaticScriptsPath))
        .pipe(notify({ message: 'Vendor scripts bundle task complete' }));
});

/// Clean
//gulp.task('clean', function (cb) {
//    del([siteStaticScriptsPath, siteStaticCssPath, siteStaticPath + '/images'], { force: true }, cb)
//});

// Default task
gulp.task('default', function () {
    gulp.start('sass', 'scripts');
});

 //Watch
gulp.task('watch', function () {

    // Watch .scss files
    gulp.watch('wwwroot/src/styles/**/*.scss', ['sass']);

    // Watch .js files
    gulp.watch('wwwroot/src/scripts/**/*.js', ['scripts']);

});
