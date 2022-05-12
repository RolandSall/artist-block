import configureQueryClient from "./queryClient";

const {
    queryClient,
    QueryClientProvider,
    hooks,
    ReactQueryDevtools,
    useMutation,
    API_ROUTES,
} = configureQueryClient({
    // notifier,
});

export {
    queryClient,
    useMutation,
    QueryClientProvider,
    hooks,
    ReactQueryDevtools,
    API_ROUTES,
};
