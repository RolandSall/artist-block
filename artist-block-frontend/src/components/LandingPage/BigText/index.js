import React from "react";
import styled from "styled-components";
import { Controller, Scene } from "react-scrollmagic";
import { Tween } from "react-gsap";
import classnames from "classnames";

const BigTextStyle = styled.div`
  .big-text-mobile {
    position: absolute;
    font-size: 450px;
    left: 100%;
    font-weight: bold;
    color: #e2e2e2;
    margin: 0 auto;
  }
`;

const BigText = props => (
    <BigTextStyle>
        <Controller>
            <Scene triggerElement="#big-text-trigger" duration={1000}>
                {progress => (
                    <Tween
                        to={{
                            left: "-200%"
                        }}
                        ease="Strong.easeInOut"
                        totalProgress={progress}
                        paused
                    >
                        <div
                            className={classnames("big-text-mobile opacity-5", props.bigText)}
                        >
                            touch
                        </div>
                    </Tween>
                )}
            </Scene>
        </Controller>
    </BigTextStyle>
);

export default BigText;
