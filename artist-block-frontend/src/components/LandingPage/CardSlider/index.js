import React from "react";
import Slider from "react-slick";
import Card from "../Card";

class CardSlider extends React.Component {
    render() {
        const settings = {
            infinite: false,
            variableWidth: true,
            centerMode: true,
            speed: 500,
            arrows: false,
            dots: false,
            slidesToShow: 1,
            slidesToScroll: 1
        };

        return (
            <div className="card-slider flex w-full sm:w-11/12 overflow-hidden">
                <div className="w-full card-slider-cont">
                    <div className="image-slider-container">
                        <div className="image-slider">
                            <Slider {...settings}>
                                <Card cardBackground="slider-card" />
                                <Card cardBackground="slider-card article-card__background-image" />
                                <Card cardBackground="slider-card" />
                                <Card cardBackground="slider-card article-card__background-image" />
                            </Slider>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default CardSlider;
