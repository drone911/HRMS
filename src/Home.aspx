<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <link rel="icon" type="image/x-icon" href="assets/img/favicon.ico" />
        <script src="https://use.fontawesome.com/releases/v5.15.1/js/all.js" crossorigin="anonymous"></script>
        <link href="https://fonts.googleapis.com/css?family=Varela+Round" rel="stylesheet" />
        <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
        <link href="Scripts/css/style.css" rel="stylesheet" />
    <div id="page-top">
        <!-- Masthead-->
        <header class="masthead">
            <div class="container d-flex h-100 align-items-center">
                <div class="mx-auto text-center">
                    <h1 class="mx-auto my-0 text-uppercase">Employee Training and Recruitment System</h1>
                    <h2 class="text-white-50 mx-auto mt-2 mb-5"></h2>
                    <a class="btn btn-primary js-scroll-trigger" href="#about">Get Started</a>
                </div>
            </div>
        </header>
        <!-- About-->
        <section class="about-section text-center" id="about">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 mx-auto">
                        <h2 class="text-white mb-4">Reliable and Easy to Use</h2>
                        <p class="text-white-50">
                            The system makes it easy to manage recruitment and training process.
                            It aim at managing Human resources in day to day life.
                            
                        </p>
                    </div>
                </div>
                <img class="img-fluid" src="assets/feature.png" alt="" />
            </div>
        </section>
        <!-- Projects-->
        <section class="projects-section bg-light" id="projects">
            <div class="container">
                <!-- Featured Project Row-->
                <div class="row align-items-center no-gutters mb-4 mb-lg-5">
                    <div class="col-xl-8 col-lg-7"><img class="img-fluid mb-3 mb-lg-0" src="assets/img/feature.jpg" alt="" /></div>
                    <div class="col-xl-4 col-lg-5">
                        <div class="featured-text text-center text-lg-left">
                            <h4>Training</h4>
                            <p class="mb-0 text-black-50">Manage training for every employee made easy.Add,Remove and assocaite it with any empployee</p>
                            <p class="text-black-50 mb-0"></p>
                        </div>
                    </div>
                </div>
                <!-- Project One Row-->
                <div class="row justify-content-center no-gutters mb-5 mb-lg-0">
                    <div class="col-lg-6"><img class="img-fluid" src="assets/img/feature1.jpg" alt="" /></div>
                    <div class="col-lg-6">
                        <div class="bg-white text-center h-100 project">
                            <div class="d-flex h-100">
                                <div class="project-text w-100 my-auto text-center text-lg-left">
                                    <h4 class="text-blue">Recruitment</h4>
                                    <p class="mb-0 text-black-50">Job Oppurtunities, Currently available jobs and Applied Posts handled reliably.</p>
                                    <hr class="d-none d-lg-block mb-0 ml-0" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Project Two Row-->
                <div class="row justify-content-center no-gutters">
                    <div class="col-lg-6"><img class="img-fluid" src="assets/img/feature2.jpg" alt="" /></div>
                    <div class="col-lg-6 order-lg-first">
                        <div class="bg-white text-center h-100 project">
                            <div class="d-flex h-100">
                                <div class="project-text w-100 my-auto text-center text-lg-right">
                                    <h4 class="text-black">Employee Management</h4>
                                    <p class="mb-0 text-black-50">Manages Employees, their roles and training sessions with verification.</p>
                                    <hr class="d-none d-lg-block mb-0 mr-0" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Signup-->
        <section class="signup-section" id="signup">
            <div class="container">
                <div class="row">
                    <div class="col-md-10 col-lg-8 mx-auto text-center">
                        <i class="far fa-paper-plane fa-2x mb-2 text-white"></i>
                        <h2 class="text-white mb-5">Subscribe to receive updates!</h2>
                        <form class="form-inline d-flex">
                            <input class="form-control flex-fill mr-0 mr-sm-2 mb-3 mb-sm-0" id="inputEmail" type="email" placeholder="Enter email address..." />
                            <button class="btn btn-primary mx-auto" type="submit">Subscribe</button>
                        </form>
                    </div>
                </div>
            </div>
        </section>
        <!-- Contact-->
        <section class="contact-section bg-black">
            <div class="container" id="cont">
                <div class="row">
                    <div class="col-md-4 mb-3 mb-md-0">
                        <div class="card py-4 h-100">
                            <div class="card-body text-center">
                                <i class="fas fa-map-marked-alt text-primary mb-2"></i>
                                <h4 class="text-uppercase m-0">Address</h4>
                                <hr class="my-4" />
                                <div class="small text-black-50">4923 Market Street, Orlando FL</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3 mb-md-0">
                        <div class="card py-4 h-100">
                            <div class="card-body text-center">
                                <i class="fas fa-envelope text-primary mb-2"></i>
                                <h4 class="text-uppercase m-0">Email</h4>
                                <hr class="my-4" />
                                <div class="small text-black-50"><a href="#!">hello@yourdomain.com</a></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3 mb-md-0">
                        <div class="card py-4 h-100">
                            <div class="card-body text-center">
                                <i class="fas fa-mobile-alt text-primary mb-2"></i>
                                <h4 class="text-uppercase m-0">Phone</h4>
                                <hr class="my-4" />
                                <div class="small text-black-50">+1 (555) 902-8832</div>
                            </div>
                        </div>
                    </div>
                </div><br/><br/>

                <!--Next Page -->
                <header class="Log">
            <div class="container d-flex h-100 align-items-center">
                <div class="mx-auto text-center">
                   <a class="btn btn-primary js-scroll-trigger" href="Login.aspx">Next Page</a>
                </div>
            </div>
        </header>
                <div class="social d-flex justify-content-center">
                    <a class="mx-2" href="#!"><i class="fab fa-twitter"></i></a>
                    <a class="mx-2" href="#!"><i class="fab fa-facebook-f"></i></a>
                    <a class="mx-2" href="#!"><i class="fab fa-github"></i></a>
                </div>
            </div>
        </section>
        <!-- Footer-->
        <footer class="footer bg-black small text-center text-white-50"><div class="container">Copyright © Your Website 2020</div></footer>
        <!-- Bootstrap core JS-->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js"></script>
        <!-- Third party plugin JS-->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.min.js"></script>
        <!-- Core theme JS-->
        <script src="js/scripts.js"></script>
    </div>
</asp:Content>

