<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            margin: 0;
        }

        html {
            box-sizing: border-box;
        }

        *, *:before, *:after {
            box-sizing: inherit;
        }

        .column {
            float: left;
            width: 33.3%;
            margin-bottom: 16px;
            padding: 0 8px;
        }

        .card {
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            margin: 8px;
        }

        .about-section {
            padding: 50px;
            text-align: center;
            background-color: black;
            color: white;
        }

        .container {
            padding: 0 16px;
        }

            .container::after, .row::after {
                content: "";
                clear: both;
                display: table;
            }

        .title {
            color: grey;
        }

        .button {
            border: none;
            outline: 0;
            display: inline-block;
            padding: 8px;
            color: white;
            background-color: #000;
            text-align: center;
            cursor: pointer;
            width: 100%;
        }

            .button:hover {
                background-color: #555;
            }

        @media screen and (max-width: 650px) {
            .column {
                width: 100%;
                display: block;
            }
        }
    </style>


    <div class="about-section">
        <p>We aim at giving reliable hrms services.</p>
        <p>We are further thinking to extend the services on android platform.</p>
    </div>
    <div>
        <br />
        <br />
    </div>
    <h2 style="text-align: center">Our Team</h2>
    <div class="row">
        <div class="column">
            <div class="card">
                <img src="/w3images/team1.jpg" alt="Jane" style="width: 100%">
                <div class="container">
                    <h2>Jane Doe</h2>
                    <p class="title">CEO & Founder</p>
                    <p>EverReady for new challenges.</p>
                    <p>jane@example.com</p>

                </div>
            </div>
        </div>

        <div class="column">
            <div class="card">
                <img src="/w3images/team2.jpg" alt="Mike" style="width: 100%">
                <div class="container">
                    <h2>Mike Ross</h2>
                    <p class="title">Art Director</p>
                    <p>Loves to play with colors.</p>
                    <p>mike@example.com</p>

                </div>
            </div>
        </div>

        <div class="column">
            <div class="card">
                <img src="/w3images/team3.jpg" alt="John" style="width: 100%">
                <div class="container">
                    <h2>John Doe</h2>
                    <p class="title">Designer</p>
                    <p>His passion lead to everychaning views.</p>
                    <p>john@example.com</p>
                    <p></p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
