import React from "react";
import "../styles/Layout.scss";
import { Button } from "@material-ui/core";
import { Menu, ExitToApp, LocalMall } from "@material-ui/icons";
import Router from "next/router";
import Cookie from "js-cookie";

function openSidebar() {
  document.getElementById("sideMenu").style.display = "block";
}
function closeSidebar() {
  document.getElementById("sideMenu").style.display = "none";
}

function logOut() {
  Cookie.set("loged", false);
  Router.push("/login");
}

const Layout = props => {
  return (
    <div>
      <div className="header">
        <div className="headerContent">
          <Button className="btnColor" onClick={openSidebar}>
            <Menu />
          </Button>
        </div>
      </div>
      <div className="sidebar">
        <div className="itemMenu">
          <div className="sidebarTitle">
            <img
              className="logo"
              src="https://miraclecbdproducts.com/images/cbd-logo.png"
              width="120"
              alt="logo"
            />
          </div>
          <div className="item">
            <div className="itemIcon">
              <LocalMall />
            </div>
            <div className="itemText">Product</div>
          </div>
          <div className="item" onClick={logOut}>
            <div className="itemIcon">
              <ExitToApp />
            </div>
            <div className="itemText">Logout</div>
          </div>
        </div>
      </div>
      <div className="main">{props.children}</div>
      <div id="sideMenu" className="modalMenu" style={{ display: "none" }}>
        <div className="modalMenuContent" onClick={closeSidebar}>
          <div className="modalSidebar">
            <div className="itemMenu">
              <div className="sidebarTitle">
                <img
                  className="logo"
                  src="https://miraclecbdproducts.com/images/cbd-logo.png"
                  width="120"
                  alt="logo"
                />
              </div>
              <div className="item">
                <div className="itemIcon">
                  <LocalMall />
                </div>
                <div className="itemText">Product</div>
              </div>
              <div className="item" onClick={logOut}>
                <div className="itemIcon">
                  <ExitToApp />
                </div>
                <div className="itemText">Logout</div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <style jsx>{`
      :global(body) {
        margin: 0;
        font-size: 1rem;
        font-family: "Roboto", "Helvetica", "Arial", sans-serif;
        font-weight: 400;
        line-height: 1.5;
        letter-spacing: 0.00938em;
        color: rgba(0, 0, 0, 0.87);
    `}</style>
    </div>
  );
};

export default Layout;
