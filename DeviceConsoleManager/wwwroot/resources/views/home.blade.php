<!DOCTYPE html>
<html lang="en" ng-app="app">
<!-- Mirrored from colorlib.com//polygon/adminty/default/ by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 28 Nov 2018 05:31:20 GMT -->
<!-- Added by HTTrack --><meta http-equiv="content-type" content="text/html;charset=UTF-8" /><!-- /Added by HTTrack -->
<head>
    <title>Cheese cake ICT console </title>
    <!-- HTML5 Shim and Respond.js IE10 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 10]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
      <![endif]-->
    <!-- Meta -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="#">
    <meta name="keywords" content="Admin , Responsive, Landing, Bootstrap, App, Template, Mobile, iOS, Android, apple, creative app">
    <meta name="author" content="#">
    <!-- Favicon icon -->
    <link rel="icon" href="https://colorlib.com//polygon/adminty/files/assets/images/favicon.ico" type="image/x-icon">
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,600" rel="stylesheet">
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="{{asset('/files/bower_components/bootstrap/css/bootstrap.min.css')}}">
    <!-- feather Awesome -->
    <link rel="stylesheet" type="text/css" href="{{asset('/files/assets/icon/feather/css/feather.css')}}">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="{{asset('/files/assets/css/style.css')}}">
    <link rel="stylesheet" type="text/css" href="{{asset('/files/assets/css/jquery.mCustomScrollbar.css')}}">
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2019.1.220/styles/kendo.common.min.css"/>
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2019.1.220/styles/kendo.rtl.min.css"/>
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2019.1.220/styles/kendo.silver.min.css"/>
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2019.1.220/styles/kendo.mobile.all.min.css"/>

</head>

<body>
    <!-- Pre-loader start -->
    <div class="theme-loader">
        <div class="ball-scale">
            <div class='contain'>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
                <div class="ring">
                    <div class="frame"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- Pre-loader end -->
    <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box"></div>
        <div class="pcoded-container navbar-wrapper">

            <nav class="navbar header-navbar pcoded-header">
                <div class="navbar-wrapper">

                    <div class="navbar-logo">
                        <a class="mobile-menu" id="mobile-collapse" href="#!">
                            <i class="feather icon-menu"></i>
                        </a>
                        <a href="index-2.html">
                            <img class="img-fluid" src="{{asset('/files/assets/images/logo.png')}}" alt="Theme-Logo" />
                        </a>
                        <a class="mobile-options">
                            <i class="feather icon-more-horizontal"></i>
                        </a>
                    </div>

                    <div class="navbar-container container-fluid">
                        <ul class="nav-left">
                            <li class="header-search">
                                <div class="main-search morphsearch-search">
                                    <div class="input-group">
                                        <span class="input-group-addon search-close"><i class="feather icon-x"></i></span>
                                        <input type="text" class="form-control">
                                        <span class="input-group-addon search-btn"><i class="feather icon-search"></i></span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <a href="#!" onclick="javascript:toggleFullScreen()">
                                    <i class="feather icon-maximize full-screen"></i>
                                </a>
                            </li>
                        </ul>
                        
                        <ul class="nav-right">
                            <li class="user-profile header-notification">
                                <div class="dropdown-primary dropdown">
                                    <div class="dropdown-toggle" data-toggle="dropdown">
                                        <img src="{{asset('/files/assets/images/avatar-4.png')}}" class="img-radius" alt="User-Profile-Image">
                                        <span>{{ Auth::user()->name }}</span>
                                        <i class="feather icon-chevron-down"></i>
                                    </div>
                                    <ul class="show-notification profile-notification dropdown-menu" data-dropdown-in="fadeIn" data-dropdown-out="fadeOut">
                                        <li>
                                            <a href="auth-normal-sign-in.html">
                                                <i class="feather icon-log-out"></i> Edit Profile
                                            </a>
                                        </li>
                                        <li>
                                            <a href="auth-normal-sign-in.html">
                                                <i class="feather icon-log-out"></i> Change Password
                                            </a>
                                        </li>
                                        <li>
                                            <a onclick="logout()">
                                                <i class="feather icon-log-out"></i> Logout
                                            </a>
                                        </li>
                                    </ul>

                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

         

            <!-- Sidebar inner chat end-->
            <div class="pcoded-main-container">
                <div class="pcoded-wrapper">
                    <nav class="pcoded-navbar">
                        <div class="pcoded-inner-navbar main-menu">
                            <div class="pcoded-navigatio-lavel">Navigation</div>
                            <ul class="pcoded-item pcoded-left-item">
                                <li class="active pcoded-trigger">
                                    <a href="#!/" >
                                        <span class="pcoded-micon"><i class="feather icon-home"></i></span>
                                        <span class="pcoded-mtext">Dashboard</span>
                                    </a>
                                </li>
                             </ul> 

                             <ul class="pcoded-item pcoded-left-item">
                                <li class="pcoded-hasmenu ">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="feather icon-users"></i></span>
                                        <span class="pcoded-hasmenu pcoded-mtext">User Settings</span>
                                    </a>

                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="#!/">
                                                <span class="pcoded-mtext">User Information</span>
                                            </a>
                                        </li>

                                        <!-- <li class="">
                                            <a href="#!/UserAccount">
                                                <span class="pcoded-mtext">User Account</span>
                                            </a>
                                        </li> -->

                                        <li class="">
                                            <a href="#!/UserRole">
                                                <span class="pcoded-mtext">User Role</span>
                                            </a>
                                        </li>
                                    </ul>
                                 
                                </li>
                             </ul>


                             <ul class="pcoded-item pcoded-left-item">
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-hasmenu pcoded-micon"><i class="feather icon-sliders"></i></span>
                                        <span class="pcoded-mtext">Device Settings</span>
                                    </a>

                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="#!/DeviceInformation">
                                                <span class="pcoded-mtext">Device Information</span>
                                            </a>
                                        </li>

                                        <!-- <li class="">
                                            <a href="#!/DeviceService">
                                                <span class="pcoded-mtext">User Device Services</span>
                                            </a>
                                        </li> -->

                                        <li class="">
                                            <a href="#!/DeviceServiceConfiguration">
                                                <span class="pcoded-mtext">Device Service Configuration</span>
                                            </a>
                                        </li>
                                        
                                    </ul>
                                   
                                </li>
                             </ul>


                        </div>
                    </nav>
                    <div class="pcoded-content">
                        <div class="pcoded-inner-content">
                            <div class="main-body">
                                <div class="page-wrapper">

                                    <div class="page-body">
                                      <div class="row">
                                            <div ng-view></div>
                                      </div>
                                    </div>
                                </div>

                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Warning Section Starts -->
    <!-- Older IE warning message -->
    <!--[if lt IE 10]>
<div class="ie-warning">
    <h1>Warning!!</h1>
    <p>You are using an outdated version of Internet Explorer, please upgrade <br/>to any of the following web browsers to access this website.</p>
    <div class="iew-container">
        <ul class="iew-download">
            <li>
                <a href="http://www.google.com/chrome/">
                    <img src="/files/assets/images/browser/chrome.png" alt="Chrome">
                    <div>Chrome</div>
                </a>
            </li>
            <li>
                <a href="https://www.mozilla.org/en-US/firefox/new/">
                    <img src="/files/assets/images/browser/firefox.png" alt="Firefox">
                    <div>Firefox</div>
                </a>
            </li>
            <li>
                <a href="http://www.opera.com">
                    <img src="/files/assets/images/browser/opera.png" alt="Opera">
                    <div>Opera</div>
                </a>
            </li>
            <li>
                <a href="https://www.apple.com/safari/">
                    <img src="/files/assets/images/browser/safari.png" alt="Safari">
                    <div>Safari</div>
                </a>
            </li>
            <li>
                <a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie">
                    <img src="/files/assets/images/browser/ie.png" alt="">
                    <div>IE (9 & above)</div>
                </a>
            </li>
        </ul>
    </div>
    <p>Sorry for the inconvenience!</p>
</div>
<![endif]-->
    <!-- Warning Section Ends -->
    <!-- Required Jquery -->
    
    <script type="text/javascript" src="{{asset('/files/bower_components/jquery/js/jquery.min.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/bower_components/jquery-ui/js/jquery-ui.min.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/bower_components/popper.js/js/popper.min.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/bower_components/bootstrap/js/bootstrap.min.js')}}"></script>
    <!-- jquery slimscroll js -->
    <script type="text/javascript" src="{{asset('/files/bower_components/jquery-slimscroll/js/jquery.slimscroll.js')}}"></script>
    <!-- modernizr js -->
    <script type="text/javascript" src="{{asset('/files/bower_components/modernizr/js/modernizr.js')}}"></script>
    <!-- Chart js -->
    <script type="text/javascript" src="{{asset('/files/bower_components/chart.js/js/Chart.js')}}"></script>
    <!-- amchart js -->
    <script src="{{asset('/files/assets/pages/widget/amchart/amcharts.js')}}"></script>
    <script src="{{asset('/files/assets/pages/widget/amchart/serial.js')}}"></script>
    <script src="{{asset('/files/assets/pages/widget/amchart/light.js')}}"></script>
    <script src="{{asset('/files/assets/js/jquery.mCustomScrollbar.concat.min.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/SmoothScroll.js')}}"></script>
    <script src="{{asset('/files/assets/js/pcoded.min.js')}}"></script>
    <!-- custom js -->
    <script src="{{asset('/files/assets/js/vartical-layout.min.js')}}"></script>
    
    <script type="text/javascript" src="{{asset('/files/assets/js/script.min.js')}}"></script>


    <!-- anuglar js -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular-route.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular-animate.js"></script>
        
    <script src="https://kendo.cdn.telerik.com/2019.1.220/js/kendo.all.min.js"></script>

    <script type="text/javascript" src="{{asset('/files/assets/js/app/module.js')}}"></script>

    <!-- controller -->
    <script type="text/javascript" src="{{asset('/files/assets/js/app/controller/chipsetcontroller.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/controller/devicecontroller.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/controller/usercontroller.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/controller/userRoleController.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/controller/deviceConfigGroupController.js')}}"></script>

    
    <!-- services -->
    <script type="text/javascript" src="{{asset('/files/assets/js/app/services/chipsetservices.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/services/deviceservices.js')}}"></script>
    <script type="text/javascript" src="{{asset('/files/assets/js/app/services/userservices.js')}}"></script>

    <script>
        function distroytoken(){
            localStorage.removeItem('token');
        }

        function getToken(){
            return  'Bearer '+ localStorage.getItem('token');
        }

        function baseApiURL(){
            return 'http://localhost:81/lsapp/public/'
        }

        function logout(){
            var csrf_token = $('meta[name="csrf-token"]').attr('content');
            var url  = baseApiURL()+'logout';

            var data = { 
                _token: csrf_token
            }
            var promise = $.post(url,function(data){
                window.location.href = baseApiURL();
                distroytoken();
        });

        }

    </script>
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-23581568-13"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'UA-23581568-13');
</script>
</body>


<!-- Mirrored from colorlib.com//polygon/adminty/default/ by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 28 Nov 2018 05:32:10 GMT -->
</html>
