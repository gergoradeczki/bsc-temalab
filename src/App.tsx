import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {
    createTheme,
    CssBaseline, ThemeProvider,
    useMediaQuery
} from "@mui/material";
import React from "react";
import {Header} from "./components/Header";
import {Board} from "./components/Board";

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
            <Header title="Témalabor: TODO Alkalmazás" />
            <Board />
        </ThemeProvider>
    )
}