echo "Starting"

docker build -f staging.Dockerfile -t rolandsall24/haqq-staging-env:1.0.4 .

docker tag rolandsall24/haqq-staging-env:1.0.0 registry.heroku.com/haqq-staging/web

docker push registry.heroku.com/haqq-staging/web

heroku container:release web -a haqq-staging
