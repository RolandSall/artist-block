import * as yup from "yup";
import {onlyLetters} from "./constants";
import moment from "moment";
import {COUNTRIES} from "../constants/countries";
import {parsePhoneNumberFromString} from "libphonenumber-js/max";

export const painterSchema = {
    personalInfo: yup.object().shape({
        firstName: yup
            .string()
            .matches(
                onlyLetters.regex,
                onlyLetters.error
            )
            .required('Please enter your first name')
        ,
        lastName: yup
            .string()
            .matches(
                onlyLetters.regex,
                onlyLetters.error
            )
            .required('Please enter your last name')
        ,
        middleName: yup
            .string()
            .matches(
                onlyLetters.regex,
                onlyLetters.error
            )
        ,
        title: yup
            .string()
            .required('Please enter your Title')
        ,
        birthDate: yup
            .string()
            .test(
                "birthDate",
                "You should be older than 18",
                value => {
                    return moment().diff(moment(value),'years') >= 18;
                }
            )
            .required('Please enter your birth date')
        ,
        nationality: yup
            .string()
            .oneOf(
                COUNTRIES.map((e) => e.label)
            )
            .required('Please enter your Nationality')
        ,
        address: yup
            .string()
            .required('Please enter your Address')
        ,
        phoneNumber: yup.object()
            .test("phoneNumber",
                "This Phone Number isn't Valid",
                ({number,metadata}) => {
                    if(number && metadata){
                        return parsePhoneNumberFromString(number, metadata.countryCode.toUpperCase()).isValid()
                    }
                    return false
                })
            .required("Please enter your phone number"),



        bio: yup
            .string()
    }),


    specializations: yup.object().shape({
        firstSpecialization: yup.string().required("Please enter at least one Specialization"),
        secondSpecialization: yup.string(),

    })
};



export const painterInitialValue = {
    "firstName": "",
    "lastName": "",
    "middleName": "",
    "title": "",
    "birthDate": moment().subtract(18,'years').format("YYYY-MM-DD"),
    "nationality": "",
    "bio": "",
    "phoneNumber":  {
        number:'',
        metadata:{}
    },
    "languages":[]
}
