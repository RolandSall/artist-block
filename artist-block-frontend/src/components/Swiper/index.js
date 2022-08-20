import React, { useRef, useState } from "react";
// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";
import "swiper/css/free-mode";
import "swiper/css/navigation";
import "swiper/css/thumbs";

import "./styles.css";

// import required modules
import { FreeMode, Navigation, Thumbs } from "swiper";

export default function CustomSwiper() {
    const [thumbsSwiper, setThumbsSwiper] = useState(null);



    return (
        <>
            <Swiper
                onSwiper={setThumbsSwiper}
                loop={true}
                spaceBetween={10}
                slidesPerView={4}
                freeMode={true}
                watchSlidesProgress={true}
                modules={[FreeMode, Navigation, Thumbs]}
                className="mySwiper"
            >
                <SwiperSlide>
                    <img src="https://artistblockstorage.blob.core.windows.net/paintings/Red and White605964a7-940c-405e-a272-af60da022962.jpg" />
                </SwiperSlide>
                <SwiperSlide>
                    <img src="https://artistblockstorage.blob.core.windows.net/paintings/white rose in the sea.jpg" />
                </SwiperSlide>
            </Swiper>
        </>
    );
}
