import {CURRENT_ARTIST_KEY, CURRENT_MEMBER_KEY, MUTATION_KEYS, PAINTER_PAINTING_KEY} from "../config/keys";
import * as Api from '../api';

export default (queryClient, queryConfig) => {

    queryClient.setMutationDefaults(MUTATION_KEYS.REGISTER_MEMBER, {
        mutationFn: async ({token, payload}) =>
            Api.registerClient(await token,payload, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_MEMBER_IMAGE, {
        mutationFn: async ({token, formData}) =>
            Api.postClientImage(await token,formData, queryConfig).then((image) => (image)),
        onSuccess: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
            queryClient.invalidateQueries(CURRENT_ARTIST_KEY);
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.REGISTER_ARTIST, {
        mutationFn: async ({token, payload}) =>
            Api.registerArtist(await token,payload, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
            queryClient.invalidateQueries(CURRENT_ARTIST_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.UPDATE_ARTIST, {
        mutationFn: async ({token, payload}) =>
            Api.updateArtist(await token,payload, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
            queryClient.invalidateQueries(CURRENT_ARTIST_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.UPDATE_MEMBER, {
        mutationFn: async ({token, payload}) =>
            Api.updateMember(await token,payload, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
            queryClient.invalidateQueries(CURRENT_ARTIST_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.UPDATE_SPECIALIZATIONS, {
        mutationFn: async ({token, payload}) =>
            Api.updateArtistSpecializations(await token,payload, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
            queryClient.invalidateQueries(CURRENT_ARTIST_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_BUY, {
        mutationFn: async ({token, paintingId}) =>
            Api.buyPainting(token,paintingId, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            // queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_GAN, {
        mutationFn: async ({ description}) =>
            Api.createGan(description, queryConfig).then((member) => (member)),
        onSuccess: data => {
            console.log(data)
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            // queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_IMAGE, {
        mutationFn: async ({ token,paintingId,formData }) =>
            Api.postPaintingImage(await token,paintingId,formData, queryConfig).then((member) => (member)),
        onSuccess: data => {

        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            // queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_PAINTING, {
        mutationFn: async ({ token,payload,painterId }) =>
            Api.postPainting(await token,painterId,payload, queryConfig).then((member) => (member)),
        onSuccess: data => {
            const temp = queryClient.getQueryData(PAINTER_PAINTING_KEY)
            queryClient.setQueryData(PAINTER_PAINTING_KEY, [...(temp || []),data])
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            // queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });
}
