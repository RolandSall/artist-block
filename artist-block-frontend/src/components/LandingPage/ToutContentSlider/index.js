import React from "react";
import ToutContent from "../ToutContent";
import OnVisible, { setDefaultProps } from "react-on-visible";

export default function ToutContentSlider(props) {
    return (
        <OnVisible
            visibleClassName="appear"
            percent={80}
            className="w-full relative toutContent-bg-purple"
        >
            <ToutContent textDirection="toutContent w-full text-left flex-row-reverse text-white" />
        </OnVisible>
    );
}
