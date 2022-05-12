import React from "react";
import Fade from "react-reveal/Fade";
import { Parallax } from "react-scroll-parallax";
import BigText from "../BigText";



const BigTextLittleText = props => (
    <div className="w-full relative">
        <div id="big-text-trigger" />
        <Parallax
            className="leading-none absolute left-0 right-0 pb-32 hidden sm:block"
            y={[50, -10]}
            tagOuter="figure"
        >
            <div className="background-big-text relative text-center font-bold mx-auto my-0 opacity-5">
                touch
            </div>
        </Parallax>
        <BigText bigText="block sm:hidden" />
        <Fade bottom>
            <div className="foreground-little-text relative left-0 right-0 text-2xl sm:text-4xl text-center font-bold mx-auto my-0 p-2 top-0">
                Sed ut perspiciatis unde omnis iste natus error sit voluptatem
                accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab
                illo inventore veritatis et quasi architecto beatae vitae dicta sunt
                explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut
                odit aut fugit, sed quia consequuntur magni dolores eos qui ratione
                voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum
                <div id="line-trigger" />
            </div>
        </Fade>
    </div>
);

export default BigTextLittleText;
