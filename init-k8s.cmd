@echo off

REM -- Postgres
kubectl apply -f postgres-pay-configmap.yaml
kubectl apply -f postgres-pay-secrets.yaml
kubectl apply -f postgres-pay-service.yaml
kubectl apply -f postgres-pay.yaml

REM -- App
kubectl apply -f app-pay-service.yaml
kubectl apply -f app-pay-ingress.yaml
kubectl apply -f app-pay.yaml

