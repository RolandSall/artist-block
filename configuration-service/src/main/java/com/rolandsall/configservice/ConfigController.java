package com.rolandsall.configservice;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class ConfigController {

    @Value("${encrypt.key}")
    private String Key;

   @GetMapping("/api/v1/test")
    public String Configs(){
        return "Configuration Controller Is Working Fine";
    }
}
