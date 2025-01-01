import React from "react";
import "./css/error.css"


const NotFound = () => {
    <head>
        <link
          href="https://fonts.googleapis.com/css?family=Poppins:400,700"
          rel="stylesheet"
        />
        <link
          type="text/css"
          rel="stylesheet"
          href="/css/style.css"
        />
      </head>
  return (
    <div id="notfound">

      <div className="notfound">
        <div className="notfound-404">
          <h1>404</h1>
        </div>
        <h2>Oops, lehte ei leitud!</h2>

        <a href="/">
          <span className="arrow"></span>Tagasi Avalehele
        </a>
      </div>
    </div>
  );
};

export default NotFound;
