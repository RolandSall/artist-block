
import {CURRENT_ARTIST_KEY, CURRENT_MEMBER_KEY, MUTATION_KEYS} from "../config/keys";
import * as Api from '../api';

export default (queryClient, queryConfig) => {

    queryClient.setMutationDefaults(MUTATION_KEYS.POST_SUBSCRIBE, {
        mutationFn: async ({ email}) =>
            Api.subscribe(email, queryConfig).then((member) => (member)),
        onSuccess: () => {
        },
        onError: (error, _, context) => {

        },
        onSettled: () => {
            queryClient.invalidateQueries(CURRENT_MEMBER_KEY);
        },
    });
}