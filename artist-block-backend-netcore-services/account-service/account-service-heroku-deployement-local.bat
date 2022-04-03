echo "Starting"

docker build -f staging.Dockerfile -t rolandsall24/artist-block-account-service:2.0.0 .

docker tag rolandsall24/artist-block-account-service:2.0.0 registry.heroku.com/artist-block-account-service/web

docker push registry.heroku.com/artist-block-account-service/web

heroku container:release web -a artist-block-account-service
