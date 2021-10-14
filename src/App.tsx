import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {
    createTheme,
    CssBaseline, Grid, ThemeProvider,
    useMediaQuery
} from "@mui/material";
import React from "react";
import MyAppBar from "./components/MyAppBar";
import Main from "./Main";

export default function App() {
    const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');

    const theme = React.useMemo(
        () =>
            createTheme({
                palette: {
                    mode: prefersDarkMode ? 'dark' : 'light',
                },
            }),
        [prefersDarkMode],
    );

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <MyAppBar title="Témalabor: TODO Alkalmazás" />
            <Grid container spacing={0} sx={{p: 2 }}
                  justifyContent="center">
                <Main/>
            </Grid>
        </ThemeProvider>
    )
}