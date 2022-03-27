import * as yup from "yup";
import {onlyLetters} from "./constants";
import moment from "moment";
import {COUNTRIES} from "../constants/countries";
import {parsePhoneNumberFromString, isValidPhoneNumber} from 'libphonenumber-js/max'


export const customerSchema =
    yup.object().shape({
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
        .required('Please enter your last name'),
    middleName: yup
        .string()
        .matches(
            onlyLetters.regex,
            onlyLetters.error
        ),
    title: yup
        .string()
        .required('Please choose a title')
        ,
    birthDate: yup
        .string()
        .test(
            "birthDate",
            "You should be older than 18",
            value => {
                return moment().diff(moment(value),'years') >= 18;
            }
        ).required('Please enter your birth date'),
    nationality: yup.string().oneOf(COUNTRIES.map((e) => e.label)).required('Please enter your Nationality'),
    address: yup.string().required("Please enter your address").required('Please enter your address'),
    languages: yup.array()
        .test(
            "languages",
            "Please pick at least one language",
            value => {
                return value.length!==0
            })
        .required("Please pick at least one language"),
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
    emergencyNumber: yup.object()
        .test("emergencyNumber",
            "This Phone Number isn't Valid",
            ({number,metadata}) => {
                if(number && metadata){
                    return parsePhoneNumberFromString(number, metadata.countryCode.toUpperCase()).isValid()
                }
                return false
            })
        .required("Please enter an emergency number"),
    gender: yup.string().required("Please specify your gender"),
    maritalStatus: yup.string(),
    profession: yup.string(),
    bio: yup.string()

});

export const customerInitialValue = {
    "firstName": "",
    "lastName": "",
    "middleName": "",
    "title": "",
    "nationality": "",
    "phoneNumber":  {
        number:'',
        metadata:{}
    },

    "birthDate": moment().subtract(18,'years').format("YYYY-MM-DD"),

}
